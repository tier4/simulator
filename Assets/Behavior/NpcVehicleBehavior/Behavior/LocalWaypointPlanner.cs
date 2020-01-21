using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulator.Map;
using Simulator.Utilities;

public class LocalWaypointPlanner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LocalWaypointsPublisher_ = new UniCom.Publisher<Npc.Vehicle.LocalWaypoints>(LocalWaypointsTopic);
        Spline_ = new CatmullRomSpline();
        StopLineSubscriber_ = new UniCom.Subscriber<MapLine>(StopLineTopic,StopLineCallback);
        GlobalWaypointsSubscirber_ = new UniCom.Subscriber<Npc.Vehicle.GlobalWaypoints>(GlobalWaypointsTopic,GlobalWaypointsCallback);
        VehicleStatusSub_ = new UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>(VehicleStatusTopic,VehicleStatusCallback);
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalWaypoints_ == null || VehicleStatus_ == null)
        {
            return;
        }
        Npc.Vehicle.LocalWaypoints local_waypoints = new Npc.Vehicle.LocalWaypoints();
        Vector3[] sampling_points = GetSamplingPoints();
        if(sampling_points.Length == 0)
        {
            return;
        }
        Spline_.SetPoints(sampling_points);
        Spline_.SetDividedCount(100);
        local_waypoints.spline = Spline_;
        Vector3[] waypoints = Spline_.Evaluate();
        LocalWaypointsPublisher_.Publish(local_waypoints);
    }

    void GlobalWaypointsCallback(Npc.Vehicle.GlobalWaypoints data)
    {
        GlobalWaypoints_ = data;
    }

    void StopLineCallback(MapLine data)
    {
        StopLine_ = data;
    }

    void VehicleStatusCallback(VehicleStatusInterface.VehicleStatus data)
    {
        VehicleStatus_ = data;
    }

    private void OnDestroy()
    {
        VehicleStatusSub_ = null;
        LocalWaypointsPublisher_ = null;
        GlobalWaypointsSubscirber_ = null;
        StopLineSubscriber_ = null;
    }

    private Vector3[] GetSamplingPoints()
    {
        List<Vector3> points = new List<Vector3>();
        if (GlobalWaypoints_ == null)
        {
            Vector3[] invalid_value = new Vector3[0];
            return invalid_value;
        }
        foreach (Npc.Vehicle.GlobalWaypoint waypoint in GlobalWaypoints_.waypoints)
        {
            points.Add(waypoint.point);
        }
        Vector3[] ret = new Vector3[points.Count];
        for (int i = 0; i < points.Count; i++)
        {
            ret[i] = points[i];
        }
        return ret;
    }

    private UniCom.Subscriber<Npc.Vehicle.GlobalWaypoints> GlobalWaypointsSubscirber_;
    private Npc.Vehicle.GlobalWaypoints GlobalWaypoints_;
    [SerializeField] private string GlobalWaypointsTopic;

    private UniCom.Subscriber<MapLine> StopLineSubscriber_;
    private MapLine StopLine_ = default(MapLine);
    [SerializeField] private string StopLineTopic = default(string);

    private UniCom.Publisher<Npc.Vehicle.LocalWaypoints> LocalWaypointsPublisher_;
    [SerializeField] private string LocalWaypointsTopic = default(string);

    private UniCom.Subscriber<VehicleStatusInterface.VehicleStatus> VehicleStatusSub_ 
        = default(UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>);
    private VehicleStatusInterface.VehicleStatus VehicleStatus_ 
        = default(VehicleStatusInterface.VehicleStatus);
    [SerializeField] private string VehicleStatusTopic = default(string);

    private CatmullRomSpline Spline_;
    //[SerializeField] private float SplineInterpolateDistance = 50.0f;
}
