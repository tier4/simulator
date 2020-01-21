using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Simulator.Map;
using System;
using Npc.Vehicle;

namespace QAI.BT.Custom.Npc.Vehicle.Task
{
    [CreateNodeMenu("BT/Action/NPC/Vehicle/StoppingAtStopLine")]
    public class StoppingAtStopLine : BTTaskNode
    {
        StoppingAtStopLine()
        {
            StopLineSub_ = new UniCom.Subscriber<MapLine>(StopLineCallback);
            IgnoreStopLineSub_ = new UniCom.Subscriber<MapLine>(IgnoreStopLineCallback);
            CurrentTaskPub_ = new UniCom.Publisher<CurrentTask>();
            VehicleStatusSub_ = new UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>(VehicleStatusCallback);
        }

        protected override BTGraphResult InternalRun()
        {
            if(StopLine_ == null)
            {
                return BTGraphResult.Success;
            }
            CurrentTaskPub_.Publish(new CurrentTask(CurrentTask.Tasks.StoppingAtStopLine));
            if(Mathf.Abs((float)VehicleStatus_.forward_velocity) < StopThreashold)
            {
                return BTGraphResult.Success;
            }
            return BTGraphResult.Running;
        }

        private void StopLineCallback(MapLine data)
        {
            if(IgnoreStopLine_ == null)
            {
                StopLine_ = data;
            }
            else if(StopLine_ == IgnoreStopLine_)
            {
                StopLine_ = null;
            }
            else
            {
                StopLine_ = data;
            }
            return;
        }

        private void IgnoreStopLineCallback(MapLine data)
        {
            IgnoreStopLine_ = data;
            return;
        }

        private void VehicleStatusCallback(VehicleStatusInterface.VehicleStatus data)
        {
            VehicleStatus_ = data;
        }

        public void SetVehicleStatusTopic(string vehicle_status_topic)
        {
            VehicleStatusSub_.SetTopic(vehicle_status_topic);
            return;
        }

        public void SetCurrentTaskTopic(string current_task_topic)
        {
            CurrentTaskPub_.SetTopic(current_task_topic);
            return;
        }

        public void SetStopLineTopic(string stop_line_topic)
        {
            StopLineSub_.SetTopic(stop_line_topic);
            return;
        }

        public void SetIgnoreStopLineTopic(string ignore_stop_line_topic)
        {
            IgnoreStopLineSub_.SetTopic(ignore_stop_line_topic);
            return;
        }

        private void OnDestroy()
        {
            IgnoreStopLineSub_ = null;
            StopLineSub_ = null;
            VehicleStatusSub_ = null;
            CurrentTaskPub_ = null;
        }

        private UniCom.Subscriber<MapLine> StopLineSub_;
        private MapLine StopLine_;
        private UniCom.Subscriber<MapLine> IgnoreStopLineSub_;
        private MapLine IgnoreStopLine_;
        private UniCom.Subscriber<VehicleStatusInterface.VehicleStatus> VehicleStatusSub_;
        private UniCom.Publisher<CurrentTask> CurrentTaskPub_;
        private VehicleStatusInterface.VehicleStatus VehicleStatus_;
        private float StopThreashold = 0.01f;
    }
}