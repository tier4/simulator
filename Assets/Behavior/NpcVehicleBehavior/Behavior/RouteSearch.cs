using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;
using Simulator.Map;
using System;
using Npc.Vehicle;

namespace QAI.BT.Custom.Npc.Vehicle.Task
{
    [CreateNodeMenu("BT/Action/NPC/Vehicle/RouteSearch")]
    public class RouteSearch : BTTaskNode
    {
        RouteSearch()
        {
            rand_ = new System.Random();
            route_ = new List<MapLane>(0);
            GlobalWaypointsSub_ = new UniCom.Subscriber<GlobalWaypoints>(GlobalWaypointsCallback);
            CurrentTaskPub_ = new UniCom.Publisher<CurrentTask>();
            RoutePublisher_ = new UniCom.Publisher<List<MapLane>>();
            VehicleStatusSub_ = new UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>(VehicleStatusCallback);
        }

        protected override BTGraphResult InternalRun()
        {
            CurrentTaskPub_.Publish(new CurrentTask(CurrentTask.Tasks.RouteSearch));
            if(Data_ == null || SimulatorManager.Instance == null)
            {
                return BTGraphResult.Running;
            }
            MapLane closest_lane = null;
            if (route_.Where(x => x == null).Count() >= 1 || route_.Count == 0)
            {
                 closest_lane = SimulatorManager.Instance.MapManager.GetClosestLane(Data_.position);
            }
            else
            {
                closest_lane = SimulatorManager.Instance.MapManager.GetClosestLane(Data_.position,route_);
            }
            if (Waypoints_ != null)
            {
                foreach(var lane in Waypoints_.lanes)
                {
                    if(lane == closest_lane)
                    {
                        return BTGraphResult.Success;
                    }
                }
            }

            int length = rand_.Next(3, 10);
            if(closest_lane == null || closest_lane.nextConnectedLanes.Count == 0)
            {
                return BTGraphResult.Failure;
            }
            route_ = new List<MapLane>(0);
            MapLane next_lane = closest_lane;
            for (int i=0; i<length; i++)
            {
                if(next_lane.nextConnectedLanes.Count == 0)
                {
                    break;
                }
                int selected_lane_index = rand_.Next(0,next_lane.nextConnectedLanes.Count);
                route_.Add(next_lane);
                next_lane = next_lane.nextConnectedLanes[selected_lane_index];
            }
            RoutePublisher_.Publish(route_);
            Debug.Log("Route Selected");
            return BTGraphResult.Success;
        }

        private void GlobalWaypointsCallback(GlobalWaypoints data)
        {
            Waypoints_ = data;
        }

        private void VehicleStatusCallback(VehicleStatusInterface.VehicleStatus data)
        {
            Data_ = data;
        }

        public void SetRouteTopic(string route_topic)
        {
            RoutePublisher_.SetTopic(route_topic);
        }

        public void SetVehicleStatusTopic(string vehicle_status_topic)
        {
            VehicleStatusSub_.SetTopic(vehicle_status_topic);
        }

        public void SetCurrentTaskTpic(string current_task_topic)
        {
            CurrentTaskPub_.SetTopic(current_task_topic);
        }

        public void SetGlobalWaypointsTopic(string global_waypoints_topic)
        {
            GlobalWaypointsSub_.SetTopic(global_waypoints_topic);
        }

        private void OnDestroy()
        {
            GlobalWaypointsSub_ = null;
            RoutePublisher_ = null;
            VehicleStatusSub_ = null;
            CurrentTaskPub_ = null;
        }

        private UniCom.Publisher<List<MapLane>> RoutePublisher_;
        private UniCom.Subscriber<VehicleStatusInterface.VehicleStatus> VehicleStatusSub_;
        private UniCom.Subscriber<GlobalWaypoints> GlobalWaypointsSub_;
        private GlobalWaypoints Waypoints_;
        private VehicleStatusInterface.VehicleStatus Data_ = null;
        private UniCom.Publisher<CurrentTask> CurrentTaskPub_;
        private System.Random rand_;
        private List<MapLane> route_;
    }
}