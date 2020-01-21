using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulator.Utilities;
using Simulator.Map;
using Npc.Vehicle;

namespace QAI.BT.Custom.Npc.Vehicle.Task
{
    [CreateNodeMenu("BT/Action/NPC/Vehicle/StoppedAtStopLine")]
    public class StoppedAtStopLine : BTTaskNode
    {
        StoppedAtStopLine()
        {
            IgnoreStopLinePub_ = new UniCom.Publisher<MapLine>();
            StopLineSub_ = new UniCom.Subscriber<MapLine>(StopLineCallback);
            CurrentTaskPub_ = new UniCom.Publisher<CurrentTask>();
        }

        protected override BTGraphResult InternalRun()
        {
            CurrentTaskPub_.Publish(new CurrentTask(CurrentTask.Tasks.StoppingAtStopLine));
            if (StopLine_ == null)
            {
                IgnoreStopLinePub_.Publish(null);
                return BTGraphResult.Success;
            }
            else
            {
                // If the stop line is stop sign
                if (StopLine_.isStopSign)
                {
                    IgnoreStopLinePub_.Publish(StopLine_);
                    return BTGraphResult.Success;
                }
                // If the stop line is not stop sign
                else
                {
                    if (StopLine_.currentState == MapData.SignalLightStateType.Green)
                    {
                        IgnoreStopLinePub_.Publish(null);
                        return BTGraphResult.Success;
                    }
                }
            }
            return BTGraphResult.Running;
        }

        private void StopLineCallback(MapLine data)
        {
            StopLine_ = data;
        }

        private void OnDestroy()
        {
            CurrentTaskPub_ = null;
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
            IgnoreStopLinePub_.SetTopic(ignore_stop_line_topic);
            return;
        }

        private UniCom.Publisher<CurrentTask> CurrentTaskPub_;
        private UniCom.Publisher<MapLine> IgnoreStopLinePub_;
        private UniCom.Subscriber<MapLine> StopLineSub_;
        private MapLine StopLine_;
    }
}