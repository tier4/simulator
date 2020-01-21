using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControlInterface : MonoBehaviour
{
    public class MotorCommand
    {
        public float motor_torque = 0.0f;
        public float steering_angle = 0.0f;
        public float brake_torque = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        MotorData_ = new MotorCommand();
        MotorCommandSubscriber_ = new UniCom.Subscriber<MotorCommand>("/vehicle/motor_cmd", callback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Set Steering Angele
        FrontLeftWheel.steerAngle = MotorData_.steering_angle;
        FrontRightWheel.steerAngle = MotorData_.steering_angle;

        // Set Motor Torque
        FrontLeftWheel.motorTorque = MotorData_.motor_torque;
        FrontRightWheel.motorTorque = MotorData_.motor_torque;
        RearLeftWheel.motorTorque = MotorData_.motor_torque;
        RearRightWheel.motorTorque = MotorData_.motor_torque;

        // Set Brake Torque
        FrontLeftWheel.brakeTorque = MotorData_.brake_torque;
        FrontRightWheel.brakeTorque = MotorData_.brake_torque;
        RearLeftWheel.brakeTorque = MotorData_.brake_torque;
        RearRightWheel.brakeTorque = MotorData_.brake_torque;

        // Sync Pose of the Wheels
        ApplyLocalPositionToVisuals(FrontLeftWheel,FrontLeftWheelModel);
        ApplyLocalPositionToVisuals(FrontRightWheel, FrontRightWheelModel);
        ApplyLocalPositionToVisuals(RearLeftWheel, RearLeftWheelModel);
        ApplyLocalPositionToVisuals(RearRightWheel, RearRightWheelModel);
    }

    public void callback(MotorCommand data)
    {
        MotorData_ = data;
    }

    private void OnDestroy()
    {
        MotorCommandSubscriber_ = null;
    }

    private void ApplyLocalPositionToVisuals(WheelCollider collider, GameObject visual)
    {
        if (visual == null) return;

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visual.transform.position = position;
        visual.transform.rotation = rotation;
    }

    private UniCom.Subscriber<MotorCommand> MotorCommandSubscriber_;
    public string Topic;
    private MotorCommand MotorData_;
    public WheelCollider FrontLeftWheel;
    public GameObject FrontLeftWheelModel;
    public WheelCollider FrontRightWheel;
    public GameObject FrontRightWheelModel;
    public WheelCollider RearLeftWheel;
    public GameObject RearLeftWheelModel;
    public WheelCollider RearRightWheel;
    public GameObject RearRightWheelModel;
}
