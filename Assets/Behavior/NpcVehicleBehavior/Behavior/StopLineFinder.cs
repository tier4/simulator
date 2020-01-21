using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulator.Map;
using Simulator.Utilities;

public class StopLineFinder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VehicleStatusSub_ = new UniCom.Subscriber<VehicleStatusInterface.VehicleStatus>(VehicleStatusTopic,VehicleStatusCallback);
        WaypointsSub_ = new UniCom.Subscriber<Npc.Vehicle.GlobalWaypoints>(GlobalWaypointsTopic,GlobalWaypointsCallback);
        StopLinePub_ = new UniCom.Publisher<MapLine>(StopLineTopic);
        IgnoreStopLinePub_ = new UniCom.Publisher<MapLine>(IgnoreStopLineTopic);
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalWaypoints_ != null)
        {
            if (GlobalWaypoints_.lanes.Count == 0 || VehicleStatus_ == null)
            {
                StopLinePub_.Publish(null);
                return;
            }
            updateIgnoreStopLine();
            StopLine_ = GlobalWaypoints_.lanes[0].stopLine;
            float? dist = getDistanceToStopLine();
            if (dist == null)
            {
                StopLinePub_.Publish(null);
                return;
            }
            double x = Mathf.Pow((float)VehicleStatus_.forward_velocity, 2.0f) / (2.0f * Decerelation) + Margin;
            if (dist.Value < x)
            {
                StopLinePub_.Publish(StopLine_);
                return;
            }
            else
            {
                StopLinePub_.Publish(null);
                return;
            }
        }
        return;
    }

    private void updateIgnoreStopLine()
    {
        MapLane closest_lane = SimulatorManager.Instance.MapManager.GetClosestLane(VehicleStatus_.position, GlobalWaypoints_.lanes);
        if(PreviousClosestLane_ != closest_lane && PreviousClosestLane_ != null)
        {
            IgnoreStopLinePub_.Publish(null);
        }
        PreviousClosestLane_ = closest_lane;
    }

    void VehicleStatusCallback(VehicleStatusInterface.VehicleStatus data)
    {
        VehicleStatus_ = data;
    }

    void GlobalWaypointsCallback(Npc.Vehicle.GlobalWaypoints data)
    {
        GlobalWaypoints_ = data;
    }

    private void OnDestroy()
    {
        StopLinePub_ = null;
        VehicleStatusSub_ = null;
        WaypointsSub_ = null;
    }

    private float? getDistanceToStopLine()
    {
        if(StopLine_ == null)
        {
            return null;
        }
        List<float> dists = new List<float>();
        for(int i=0; i<(StopLine_.mapWorldPositions.Count-1) ;i++)
        {
            dists.Add(Mathf.Sqrt(Utility.SqrDistanceToSegment(StopLine_.mapWorldPositions[i],StopLine_.mapWorldPositions[i+1],transform.position)));
        }
        return dists.FindMin(x => x);
    }

    private UniCom.Subscriber<Npc.Vehicle.GlobalWaypoints> WaypointsSub_;
    private MapLine StopLine_;

    private UniCom.Publisher<MapLine> StopLinePub_;
    private UniCom.Publisher<MapLine> IgnoreStopLinePub_;

    private UniCom.Subscriber<VehicleStatusInterface.VehicleStatus> VehicleStatusSub_;
    private VehicleStatusInterface.VehicleStatus VehicleStatus_;

    private Npc.Vehicle.GlobalWaypoints GlobalWaypoints_;
    private MapLane PreviousClosestLane_;

    [SerializeField] private string GlobalWaypointsTopic;
    [SerializeField] private string StopLineTopic;
    [SerializeField] private string IgnoreStopLineTopic;
    [SerializeField] private string VehicleStatusTopic;
    [SerializeField] float Decerelation = 1.0f;
    [SerializeField] float Margin = 5.0f;
}
