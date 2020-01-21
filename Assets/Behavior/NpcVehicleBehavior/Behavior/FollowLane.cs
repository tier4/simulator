using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using XNode;
using Simulator.Map;
using Simulator.Utilities;
using UniRx;
using System.Linq;
using Npc.Vehicle;

namespace QAI.BT.Custom.Npc.Vehicle.Task
{
    [CreateNodeMenu("BT/Action/NPC/Vehicle/FollowLane")]
    public class FollowLane : BTTaskNode
    {
        FollowLane()
        {
            StopLine_ = null;
            Data_ = null;
            CurrentTaskPub_ = new UniCom.Publisher<CurrentTask>();
            StopLineSub_ = new UniCom.Subscriber<MapLine>(StopLineCallback);
            VehicleStatusSub_ = new UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>(VehicleStatusCallback);
            VehicleRouteSub_ = new UniCom.Subscriber<List<MapLane>>(VehicleRouteCallback);
        }

        protected override BTGraphResult InternalRun()
        {
            CurrentTaskPub_.Publish(new CurrentTask(CurrentTask.Tasks.FollowLane));
            if (Data_ == null || SimulatorManager.Instance == null || StopLine_ == null)
            {
                return BTGraphResult.Running;
            }
            else
            {
                ClosestLane_ = SimulatorManager.Instance.MapManager.GetClosestLane(Data_.position);

                if(StopLine_.currentState == MapData.SignalLightStateType.Green)
                {
                    return BTGraphResult.Running;
                }
                else
                {
                    return BTGraphResult.Success;
                }
            }
        }

        private void OnDestroy()
        {
            StopLineSub_ = null;
            VehicleStatusSub_ = null;
            VehicleRouteSub_ = null;
            CurrentTaskPub_ = null;
        }

        public void SetCurrentTaskTopic(string current_task_topic)
        {
            CurrentTaskPub_.SetTopic(current_task_topic);
        }

        public void SetVehicleStatusTopic(string vehicle_status_topic)
        {
            VehicleStatusSub_.SetTopic(vehicle_status_topic);
        }

        public void SetVehicleRouteTopic(string vehicle_route_topic)
        {
            VehicleRouteSub_.SetTopic(vehicle_route_topic);
        }

        public void SetStopLineTopic(string stop_line_topic)
        {
            StopLineSub_.SetTopic(stop_line_topic);
        }

        private void VehicleStatusCallback(VehicleStatusInterface.VehicleStatus data)
        {
            Data_ = data;
        }

        private void VehicleRouteCallback(List<MapLane> data)
        {
            Route_ = data;
        }

        private void StopLineCallback(MapLine data)
        {
            StopLine_ = data;
        }

        private UniCom.Publisher<CurrentTask> CurrentTaskPub_;
        private UniCom.Subscriber<VehicleStatusInterface.VehicleStatus> VehicleStatusSub_;
        private UniCom.Subscriber<MapLine> StopLineSub_;
        private UniCom.Subscriber<List<MapLane>> VehicleRouteSub_;
        private VehicleStatusInterface.VehicleStatus Data_ = null;
        private MapLane ClosestLane_ = null;
        private List<MapLane> Route_ = null;
        private MapLine StopLine_;
    }
}