using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QAI;

public class NpcVehicleGraphRunner : AIGraphRunner
{

    private void Awake()
    {

    }

    private void OnEnable()
    {
        OnGraphInit += SetToppics;
    }

    private void OnDisable()
    {
        OnGraphInit -= SetToppics;
    }

    void SetToppics()
    {
        var routeSearchNode = this._graph.nodes.Where(x => x.name == "Route Search").FirstOrDefault() 
                as QAI.BT.Custom.Npc.Vehicle.Task.RouteSearch;
        if(routeSearchNode != null)
        {
            routeSearchNode.SetVehicleStatusTopic(VehicleStatusTopic);
            routeSearchNode.SetRouteTopic(VehicleRouteTopic);
            routeSearchNode.SetCurrentTaskTpic(CurrentTaskTopic);
            routeSearchNode.SetGlobalWaypointsTopic(GlobalWaypointsTopic);
        }
        var followLaneNode = this._graph.nodes.Where(x => x.name == "Follow Lane").FirstOrDefault() 
                as QAI.BT.Custom.Npc.Vehicle.Task.FollowLane;
        if (followLaneNode != null)
        {
            followLaneNode.SetVehicleStatusTopic(VehicleStatusTopic);
            followLaneNode.SetVehicleRouteTopic(VehicleRouteTopic);
            followLaneNode.SetStopLineTopic(StopLineTopic);
            followLaneNode.SetCurrentTaskTopic(CurrentTaskTopic);
        }
        var stoppingAtStopLineNode = this._graph.nodes.Where(x => x.name == "Stopping At Stop Line").FirstOrDefault() 
                as QAI.BT.Custom.Npc.Vehicle.Task.StoppingAtStopLine;
        if(stoppingAtStopLineNode != null)
        {
            stoppingAtStopLineNode.SetVehicleStatusTopic(VehicleStatusTopic);
            stoppingAtStopLineNode.SetCurrentTaskTopic(CurrentTaskTopic);
            stoppingAtStopLineNode.SetStopLineTopic(StopLineTopic);
            stoppingAtStopLineNode.SetIgnoreStopLineTopic(IgnoreStopLineTopic);
        }

        var stoppedAtStopLineNode = this._graph.nodes.Where(x => x.name == "Stopped At Stop Line").FirstOrDefault()
                as QAI.BT.Custom.Npc.Vehicle.Task.StoppedAtStopLine;
        if(stoppedAtStopLineNode != null)
        {
            stoppedAtStopLineNode.SetStopLineTopic(StopLineTopic);
            stoppedAtStopLineNode.SetCurrentTaskTopic(CurrentTaskTopic);
            stoppedAtStopLineNode.SetIgnoreStopLineTopic(IgnoreStopLineTopic);
        }
    }

    public string GlobalWaypointsTopic = "/vehicle/global_waypoints";
    public string MotorCmdTopic = "/vehicle/motor_cmd";
    public string VehicleStatusTopic = "/vehicle/status";
    public string VehicleRouteTopic = "/vehicle/route";
    public string StopLineTopic = "/vehicle/stop_line";
    public string IgnoreStopLineTopic = "/vehicle/stop_line/ignore";
    public string CurrentTaskTopic = "/vehicle/current_task";
}