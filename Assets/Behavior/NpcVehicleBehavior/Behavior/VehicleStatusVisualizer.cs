using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulator.Map;
using Npc.Vehicle;

public class VehicleStatusVisualizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CurrentTask_ = "";
        CurrentTaskSub_ = new UniCom.Subscriber<CurrentTask>(CurrentTraskTopic,CurrentTaskCallback);
        Sub_ = new UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>(VehicleStatusTopic,VehicleStatusCallback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Sub_ = null;
    }

    private void VehicleStatusCallback(VehicleStatusInterface.VehicleStatus data)
    {
        Status_ = data;
    }

    private void CurrentTaskCallback(CurrentTask data)
    {
        CurrentTask_ = data.task.ToString();
    }

    private void OnDrawGizmos()
    {
        if(Status_ == null)
        {
            return;
        }
        Gizmos.color = Color.white;
        Vector3 position = transform.InverseTransformPoint(transform.position);
        position.x = position.x - 5.0f;
        position.z = position.z - 3.0f;
        position = transform.TransformPoint(position);
        UnityEditor.Handles.Label(position,"current task : " + CurrentTask_ +
            "\nlinear velocity : " + Mathf.Ceil((float)Status_.forward_velocity*100)/100 + 
            "m/s\n angular velocity :  " + Mathf.Ceil((float)Status_.angular_velocity*100)/100 + "rad/s");
    }

    private UniCom.Subscriber<CurrentTask> CurrentTaskSub_;
    private VehicleStatusInterface.VehicleStatus Status_;
    private UniCom.Subscriber<VehicleStatusInterface.VehicleStatus> Sub_;
    [SerializeField] private string VehicleStatusTopic;
    [SerializeField] private string CurrentTraskTopic;
    private string CurrentTask_;
}