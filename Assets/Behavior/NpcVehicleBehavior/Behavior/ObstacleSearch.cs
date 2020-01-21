using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSearch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LocalWaypointsSub_ = new UniCom.Subscriber<Npc.Vehicle.LocalWaypoints>(LocalWaypointsTopic,LocalWaypointsCallback);
        VehicleStatusSub_ = new UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>(VehicleStatusTopic,VehicleStatusCallback);
    }

    // Update is called once per frame
    void Update()
    {
        if(DetectionRange_ == null || LocalWaypoints_ == null)
        {
            return;
        }
        Vector3? Position =  LocalWaypoints_.spline.GetPositionFromTargetLength(DetectionRange_.Value);
    }

    private void OnDestroy()
    {
        LocalWaypointsSub_ = null;
        VehicleStatusSub_ = null;
    }

    private void VehicleStatusCallback(VehicleStatusInterface.VehicleStatus data)
    {
        VehicleStatus_ = data;
        DetectionRange_ = (float)(data.forward_velocity / Decerelation + Margin);
    }

    private void LocalWaypointsCallback(Npc.Vehicle.LocalWaypoints data)
    {
        LocalWaypoints_ = data;
    }

    private UniCom.Subscriber<Npc.Vehicle.LocalWaypoints> LocalWaypointsSub_
        = default(UniCom.Subscriber<Npc.Vehicle.LocalWaypoints>);
    private Npc.Vehicle.LocalWaypoints LocalWaypoints_ = null;
    public string LocalWaypointsTopic = default(string);
    private UniCom.Subscriber<VehicleStatusInterface.VehicleStatus> VehicleStatusSub_
        = default(UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>);
    private VehicleStatusInterface.VehicleStatus VehicleStatus_ = null;
    public string VehicleStatusTopic = default(string);
    public float Margin = 10.0f;
    public float Decerelation = 1.0f;
    private float? DetectionRange_ = null;
}
