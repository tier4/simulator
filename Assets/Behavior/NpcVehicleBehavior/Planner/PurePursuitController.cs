using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulator.Utilities;
using System.Linq;
using Simulator.Map;

public class PurePursuitController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LinearController_ = new Simulator.Utilities.PID(300.0f,10.0f,5.0f);
        LinearController_.SetWindupGuard(1.0f);
        AngularController_ = new Simulator.Utilities.PID(1.0f, 0.0f, 0.0f);
        AngularController_.SetWindupGuard(0.01f);
        AngularController_.SetLimit(Mathf.PI*0.15f);
        MotorCommandPub_ = new UniCom.Publisher<VehicleControlInterface.MotorCommand>(MotorCommandTopic);
        VehicleStatusSub_ = new UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>(VehicleStatusTopic,VehicleStatusCallback);
        LocalWaypointsSub_ = new UniCom.Subscriber<Npc.Vehicle.LocalWaypoints>(LocalWaypointsTopic,LocalWaypointCallback);
        TargetLinearVelocitySub_ = new UniCom.Subscriber<float>(TargetLinearVelocityTopic,TargetLinearVelocityCallback);
        TargetPositionPub_ = new UniCom.Publisher<Vector3>(TargetPositionTopic);
    }

    // Update is called once per frame
    void Update()
    {
        if(VehicleStatus_ == null || Waypoints_ == null)
        {
            return;
        }
        Waypoints_.spline.SetDividedCount(100);
        Vector3[] points = Waypoints_.spline.Evaluate();
        bool target_pos_finded = false;
        for(int i=0; i<(points.Length-1); i++)
        {
            List<Vector3> crossing_points = new List<Vector3>();
            bool hit = Utility.isLineCrossingToSphere(points[i],points[i+1],transform.position,LookAheadDistance_,out crossing_points);
            if(hit)
            {
                if(crossing_points.Count == 1)
                {
                    Vector3 local_pos = transform.InverseTransformPoint(crossing_points[0]);
                    if(local_pos.z > 0.0f)
                    {
                        target_pos_finded = true;
                        TargetPosition_ = crossing_points[0];
                        break;
                    }
                }
                else if (crossing_points.Count == 2)
                {
                    Vector3 local_pos_0 = transform.InverseTransformPoint(crossing_points[0]);
                    Vector3 local_pos_1 = transform.InverseTransformPoint(crossing_points[1]);
                    if (local_pos_0.z > 0.0f)
                    {
                        target_pos_finded = true;
                        TargetPosition_ = crossing_points[0];
                        break;
                    }
                    if (local_pos_1.z > 0.0f)
                    {
                        target_pos_finded = true;
                        TargetPosition_ = crossing_points[1];
                        break;
                    }
                }
            }
        }
        if (!target_pos_finded)
        {
            TargetPosition_ = points[points.Length - 1];
            float distance = transform.InverseTransformPoint(TargetPosition_.Value).magnitude;
        }
        VehicleControlInterface.MotorCommand motor_command = new VehicleControlInterface.MotorCommand();
        Vector3 target_position_in_local = transform.InverseTransformPoint(TargetPosition_.Value);
        LinearController_.UpdateErrors(Time.deltaTime, (float)VehicleStatus_.forward_velocity, TargetLinearVelocity);
        float accel_value = LinearController_.Run();
        if (accel_value > 0.0f)
        {
            motor_command.brake_torque = 0.0f;
            motor_command.motor_torque = accel_value;
        }
        else
        {
            motor_command.brake_torque = -1 * accel_value;
            motor_command.motor_torque = 0.0f;
        }
        float a = Mathf.Sqrt(Mathf.Pow(target_position_in_local.z, 2.0f) + Mathf.Pow(target_position_in_local.x, 2.0f));
        float theta = Mathf.Atan2(target_position_in_local.x, target_position_in_local.z);
        float r = Mathf.Abs((Mathf.Sin(theta) * a) / (1 - Mathf.Cos(2 * theta)));
        float target_angular_vel = (float)VehicleStatus_.forward_velocity / (2 * r);
        if (theta < 0.0f)
        {
            target_angular_vel = target_angular_vel * -1.0f;
        }
        AngularController_.UpdateErrors(Time.deltaTime, (float)VehicleStatus_.angular_velocity, target_angular_vel);
        motor_command.steering_angle = AngularController_.Run() / Mathf.PI * 180.0f;
        MotorCommandPub_.Publish(motor_command);
    }

    void TargetLinearVelocityCallback(float data)
    {
        TargetLinearVelocity = data;
    }

    void LocalWaypointCallback(Npc.Vehicle.LocalWaypoints waypoints)
    {
        Waypoints_ = waypoints;
    }

    void VehicleStatusCallback(VehicleStatusInterface.VehicleStatus data)
    {
        VehicleStatus_ = data;
        if (VehicleStatus_.forward_velocity * LookAheadRatio <= MinimumLookAheadDistance)
        {
            LookAheadDistance_ = MinimumLookAheadDistance;
        }
        else
        {
            LookAheadDistance_ = (float)VehicleStatus_.forward_velocity * LookAheadRatio;
        }
    }

    private void OnDestroy()
    {
        LocalWaypointsSub_ = null;
        TargetPositionPub_ = null;
        VehicleStatusSub_ = null;
        TargetLinearVelocitySub_ = null;
    }

    private void OnDrawGizmos()
    {
        if(TargetPosition_ != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(TargetPosition_.Value, 0.5f);
            Gizmos.DrawWireSphere(transform.position,LookAheadDistance_);
        }
    }

    private UniCom.Subscriber<Npc.Vehicle.LocalWaypoints> LocalWaypointsSub_;
    private Npc.Vehicle.LocalWaypoints Waypoints_;
    public string LocalWaypointsTopic;
    private Vector3? TargetPosition_;
    private UniCom.Publisher<Vector3> TargetPositionPub_;
    public string TargetPositionTopic;
    public string VehicleStatusTopic;
    [SerializeField] private float WaypointSearchRadius = 20.0f;
    [SerializeField] private float LookAheadRatio = 2.0f;
    [SerializeField] private float MinimumLookAheadDistance = 4.0f;
    private float LookAheadDistance_ = 0.0f;
    private VehicleStatusInterface.VehicleStatus VehicleStatus_;
    private UniCom.Subscriber<VehicleStatusInterface.VehicleStatus> VehicleStatusSub_;
    private UniCom.Publisher<VehicleControlInterface.MotorCommand> MotorCommandPub_;
    [SerializeField] string MotorCommandTopic;

    private float TargetLinearVelocity = 0.0f;
    [SerializeField] string TargetLinearVelocityTopic;
    private UniCom.Subscriber<float> TargetLinearVelocitySub_;

    private Simulator.Utilities.PID LinearController_;
    private Simulator.Utilities.PID AngularController_;
}
