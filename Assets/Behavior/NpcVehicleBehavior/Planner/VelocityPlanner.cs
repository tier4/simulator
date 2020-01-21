using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulator.Map;
using Simulator.Utilities;

public class VelocityPlanner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IgnoreStopLineSub_ = new UniCom.Subscriber<MapLine>(IgnoreStopLineTopic,IgnoreStopLineCallback);
        CurrentTaskSub_ = new UniCom.Subscriber<Npc.Vehicle.CurrentTask>(CurrentTaskTopic,CurrentTaskCallback);
        VehicleStatusSub_ = new UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>(VehicleStatusTopic,VehicleStatusCallback);
        StopLineSub_ = new UniCom.Subscriber<MapLine>(StopLineTopic,StopLineCallback);
        LocalWaypointsSub_ = new UniCom.Subscriber<Npc.Vehicle.LocalWaypoints>(LocalWaypointsTopic,LocalWaypointsCallback);
        GlobalWaypointsSub_ = new UniCom.Subscriber<Npc.Vehicle.GlobalWaypoints>(GlobalWaypointsTopic,GlobalWaypointsCallback);   
        TargetLinearVelocityPub_ = new UniCom.Publisher<float>(TargetLinearVelocityTopic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        TargetLinearVelocityPub_ = null;
        StopLineSub_ = null;
        LocalWaypointsSub_ = null;
    }

    private void IgnoreStopLineCallback(MapLine data)
    {
        IgnoreStopLine_ = data;
    }

    private void GlobalWaypointsCallback(Npc.Vehicle.GlobalWaypoints data)
    {
        GlobalWaypoints_ = data;
    }

    private void VehicleStatusCallback(VehicleStatusInterface.VehicleStatus data)
    {
        VehicleStatus_ = data;
    }

    private void CurrentTaskCallback(Npc.Vehicle.CurrentTask task)
    {
        Task_ = task;
    }

    private void StopLineCallback(MapLine data)
    {
        if (IgnoreStopLine_ != null && IgnoreStopLine_ == data)
        {
            StopLine_ = null;
        }
        else
        {
            StopLine_ = data;
        }
    }

    private void LocalWaypointsCallback(Npc.Vehicle.LocalWaypoints data)
    {
        if(GlobalWaypoints_ == null)
        {
            TargetLinearVelocityPub_.Publish(0.0f);
            return;
        }
        if(Task_.task == Npc.Vehicle.CurrentTask.Tasks.FollowLane)
        {
            TargetLinearVelocityPub_.Publish(CruisingSpeed);
            return;
        }
        float? dist = getDistanceToStopLine();
        if (VehicleStatus_ != null)
        {
            // Stop Line was found
            if (dist != null)
            {
                decleasingAtStopline(dist);
                return;
            }
            // No Stop Line in front of the vehicle
            MapLane closest_lane = SimulatorManager.Instance.MapManager.GetClosestLane(VehicleStatus_.position,GlobalWaypoints_.lanes);
            if (closest_lane.laneTurnType == MapData.LaneTurnType.LEFT_TURN ||
                closest_lane.laneTurnType == MapData.LaneTurnType.RIGHT_TURN)
            {
                TargetLinearVelocityPub_.Publish(TurningSpeed);
                return;
            }
        }
        TargetLinearVelocityPub_.Publish(CruisingSpeed);
        return;
    }

    private void decleasingAtStopline(float? distance)
    { 
        if (Task_ == null)
        {
            TargetLinearVelocityPub_.Publish(0.0f);
            return;
        }
        if (Task_.task == Npc.Vehicle.CurrentTask.Tasks.StoppingAtStopLine)
        {
            if (StopLine_.currentState == MapData.SignalLightStateType.Red ||
            StopLine_.currentState == MapData.SignalLightStateType.Yellow)
            {
                float x = Mathf.Pow((float)VehicleStatus_.forward_velocity, 2.0f) / (2 * Deceleration) + Margin;
                if (distance.Value > x)
                {
                    TargetLinearVelocityPub_.Publish(CruisingSpeed);
                    return;
                }
                else
                {
                    if ((distance.Value - Margin) >= 0.0f)
                    {
                        float v = Mathf.Sqrt(2 * Deceleration * (distance.Value - Margin));
                        TargetLinearVelocityPub_.Publish(v);
                        return;
                    }
                    else
                    {
                        TargetLinearVelocityPub_.Publish(getCurrentTargetSpeed(0.0f));
                        return;
                    }
                }
            }
        }
    }

    private float? getDistanceToStopLine()
    {
        if (StopLine_ == null)
        {
            return null;
        }
        List<float> dists = new List<float>();
        for (int i = 0; i < (StopLine_.mapWorldPositions.Count - 1); i++)
        {
            dists.Add(Mathf.Sqrt(Utility.SqrDistanceToSegment(StopLine_.mapWorldPositions[i], 
                StopLine_.mapWorldPositions[i + 1], 
                transform.position)));
        }
        return dists.FindMin(x => x);
    }

    private UniCom.Subscriber<Npc.Vehicle.CurrentTask> CurrentTaskSub_;
    private Npc.Vehicle.CurrentTask Task_;
    public string CurrentTaskTopic;

    private UniCom.Subscriber<Npc.Vehicle.GlobalWaypoints> GlobalWaypointsSub_;
    private Npc.Vehicle.GlobalWaypoints GlobalWaypoints_;
    public string GlobalWaypointsTopic;

    private UniCom.Subscriber<VehicleStatusInterface.VehicleStatus> VehicleStatusSub_;
    private VehicleStatusInterface.VehicleStatus VehicleStatus_;
    public string VehicleStatusTopic;

    private UniCom.Subscriber<Npc.Vehicle.LocalWaypoints> LocalWaypointsSub_;
    private Npc.Vehicle.LocalWaypoints Waypoints_;
    public string LocalWaypointsTopic;
    private UniCom.Publisher<float> TargetLinearVelocityPub_;
    public string TargetLinearVelocityTopic;

    private UniCom.Subscriber<MapLine> StopLineSub_;
    private MapLine StopLine_;
    public string StopLineTopic;

    private UniCom.Subscriber<MapLine> IgnoreStopLineSub_;
    private MapLine IgnoreStopLine_;
    public string IgnoreStopLineTopic;

    float getCurrentTargetSpeed(float desired)
    {
        if(VehicleStatus_ == null || GlobalWaypoints_ == null)
        {
            return 0.0f;
        }
        MapLane closest_lane = SimulatorManager.Instance.MapManager.GetClosestLane(VehicleStatus_.position, GlobalWaypoints_.lanes);
        float limit = closest_lane.speedLimit/3.6f;
        if(closest_lane.laneTurnType == MapData.LaneTurnType.NO_TURN || closest_lane.laneTurnType == MapData.LaneTurnType.U_TURN)
        {
            if (limit > CruisingSpeed)
            {
                return Mathf.Clamp(desired, 0.0f, limit);
            }
            else
            {
                return Mathf.Clamp(desired, 0.0f, CruisingSpeed);
            }
        }
        else
        {
            if(limit > TurningSpeed)
            {
                return Mathf.Clamp(desired, 0.0f, limit);
            }
            else
            {
                return Mathf.Clamp(desired, 0.0f, CruisingSpeed);
            }
        }
    }

    public float CruisingSpeed = 5.0f;
    public float TurningSpeed = 3.0f;
    public float Deceleration = 1.0f;
    public float Margin = 3.0f;
}
