using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulator.Map;

public class GlobalWaypointVisualizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StopLineSub_ = new UniCom.Subscriber<MapLine>(StopLineTopic, StopLineCallback);
        GlobalWaypointsSub_ = new UniCom.Subscriber<Npc.Vehicle.GlobalWaypoints>(GlobalWaypointsTopic,GlobalWaypointsCallback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if(Data_ == null)
        {
            return;
        }
        int index = 0;
        foreach(Npc.Vehicle.GlobalWaypoint waypoint in Data_.waypoints)
        {
            float r = 0.0f;
            float g = 0.0f;
            if(waypoint.maximum_speed > HigherThreashold)
            {
                r = 1.0f;
                g = 0.0f;
            }
            else if(waypoint.maximum_speed < LowerThreashold)
            {
                r = 0.0f;
                g = 1.0f;
            }
            else
            {
                r = (waypoint.maximum_speed - LowerThreashold)/(HigherThreashold - LowerThreashold);
                g = (HigherThreashold - waypoint.maximum_speed)/(HigherThreashold - LowerThreashold);
            }
            Gizmos.color = new Color(r, g, 0, 0.6f);
            Gizmos.DrawSphere(waypoint.point, 0.5f);
            UnityEditor.Handles.Label(waypoint.point, "Global Waypoint \n Index:" + index);// + "\n Speed Limit: " + waypoint.maximum_speed);
            index++;
        }
        int num_waypoints = Data_.waypoints.Count;
        if (num_waypoints > 0)
        {
            Gizmos.color = Color.blue;
            //Utils.GizmosExtensions.DrawArrow(transform.position, Data_.waypoints[0].point);
            for (int i = 0; i < (num_waypoints - 1); i++)
            {
                Utils.GizmosExtensions.DrawArrow(Data_.waypoints[i].point, Data_.waypoints[i + 1].point);
            }
        }
        if(StopLine_ != null)
        {
            if(StopLine_.currentState == MapData.SignalLightStateType.Green)
            {
                Gizmos.color = Color.green;
            }
            else if(StopLine_.currentState == MapData.SignalLightStateType.Red)
            {
                Gizmos.color = Color.red;
            }
            else if(StopLine_.currentState == MapData.SignalLightStateType.Yellow)
            {
                Gizmos.color = Color.yellow;
            }
            else if(StopLine_.currentState == MapData.SignalLightStateType.Black)
            {
                Gizmos.color = Color.black;
            }
            int num_lines = StopLine_.mapWorldPositions.Count-1;
            for(int i=0; i<num_lines; i++)
            {
                Gizmos.DrawLine(StopLine_.mapWorldPositions[i],StopLine_.mapWorldPositions[i+1]);
            }
        }
    }

    private void GlobalWaypointsCallback(Npc.Vehicle.GlobalWaypoints data)
    {
        Data_ = data;
    }

    private void StopLineCallback(MapLine data)
    {
        StopLine_ = data;
    }

    private UniCom.Subscriber<Npc.Vehicle.GlobalWaypoints> GlobalWaypointsSub_;
    private Npc.Vehicle.GlobalWaypoints Data_ = null;
    [SerializeField] private string GlobalWaypointsTopic;

    [SerializeField] private float HigherThreashold = 60.0f;
    [SerializeField] private float LowerThreashold = 10.0f;

    private MapLine StopLine_;
    private UniCom.Subscriber<MapLine> StopLineSub_;
    [SerializeField] private string StopLineTopic;
}
