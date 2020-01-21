using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulator.Map;
using System;

public class GlobalWaypointPlanner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Status_ = null;
        VehicleRouteSub_ = new UniCom.Subscriber<List<MapLane>>(RouteTopic,VehicleRouteCallback);
        GlobalWaypointsPub_ = new UniCom.Publisher<Npc.Vehicle.GlobalWaypoints>(GlobalWaypointsTopic);
        VehicleStatusSub_ = new UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>(VehicleStatusTopic,VehicleStatusCallback);
    }

    private void VehicleRouteCallback(List<MapLane> data)
    {
        Route_ = data;
    }

    // Update is called once per frame
    void Update()
    {
        Transform trans = transform;
        bool found_first_waypoint = false;
        if(Route_ == null || Status_ == null)
        {
            return;
        }
        // Building Global Lanes
        Npc.Vehicle.GlobalWaypoints global_waypoints = new Npc.Vehicle.GlobalWaypoints();
        MapLane closest_lane = SimulatorManager.Instance.MapManager.GetClosestLane(Status_.position);
        foreach (MapLane lane in Route_)
        {
            if(closest_lane == lane)
            {
                foreach (Vector3 world_position in lane.mapWorldPositions)
                {
                    Vector3 local_position = trans.InverseTransformPoint(world_position);
                    float dist = Vector3.Distance(world_position, transform.position);
                    if (local_position.z > 0 && found_first_waypoint == false && dist <= WaypointSearchRadius)
                    {
                        found_first_waypoint = true;
                    }
                }
            }
            if (found_first_waypoint)
            {
                global_waypoints.lanes.Add(lane);
                if (global_waypoints.lanes.Count == MaximumLaneSize)
                {
                    break;
                }
            }
        }

        // Building Global Waypoints
        int lane_size = global_waypoints.lanes.Count;
        for(int i=0; i<lane_size; i++)
        {
            int num_waypoints = global_waypoints.lanes[i].mapWorldPositions.Count;
            for (int m = 0; m < (num_waypoints - 1); m++)
            {
                Vector3 world_position = global_waypoints.lanes[i].mapWorldPositions[m];
                Npc.Vehicle.GlobalWaypoint waypoint = new Npc.Vehicle.GlobalWaypoint();
                waypoint.point = world_position;
                waypoint.maximum_speed = global_waypoints.lanes[i].speedLimit;
                global_waypoints.waypoints.Add(waypoint);
            }
        }
        GlobalWaypointsPub_.Publish(global_waypoints);
    }

    void VehicleStatusCallback(VehicleStatusInterface.VehicleStatus data)
    {
        Status_ = data;
    }

    private void OnDestroy()
    {
        VehicleRouteSub_ = null;
        GlobalWaypointsPub_ = null;
    }

    private UniCom.Subscriber<List<MapLane>> VehicleRouteSub_;
    [SerializeField] private string RouteTopic;
    private List<MapLane> Route_;

    private UniCom.Publisher<Npc.Vehicle.GlobalWaypoints> GlobalWaypointsPub_;
    [SerializeField] private string GlobalWaypointsTopic;

    private UniCom.Subscriber<VehicleStatusInterface.VehicleStatus> VehicleStatusSub_;
    [SerializeField] private string VehicleStatusTopic;
    private VehicleStatusInterface.VehicleStatus Status_;

    [SerializeField] int MaximumLaneSize = 3;
    [SerializeField] float WaypointSearchRadius = 20.0f;
}
