using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulator.Utilities;

public class LocalWaypointVisualizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LocalWaypoints_ = null;
        LocalWaypointsSub_ = new UniCom.Subscriber<Npc.Vehicle.LocalWaypoints>(LocalWaypointTopic,LocalWaypointsCallback);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LocalWaypointsCallback(Npc.Vehicle.LocalWaypoints data)
    {
        LocalWaypoints_ = data;
    }

    private void OnDestroy()
    {
        LocalWaypointsSub_ = null;
    }

    private void OnDrawGizmos()
    {
        if (LocalWaypoints_ == null)
        {
            return;
        }
        CatmullRomSpline spline = LocalWaypoints_.spline;
        Vector3[] control_points_array = spline.GetPoints();
        Gizmos.color = Color.white;
        int count = 0;
        foreach(Vector3 point in control_points_array)
        {
            Gizmos.DrawWireCube(point, new Vector3(1.0f, 1.0f, 1.0f));
            count++;
        }
        spline.SetDividedCount(100);
        Vector3[] points_array = spline.Evaluate();
        Gizmos.color = Color.blue;
        for(int i=0; i<points_array.Length-1; i++)
        {
            Gizmos.DrawLine(points_array[i],points_array[i+1]);
        }
        /*
        foreach (Vector3 point in points_array)
        {
            Gizmos.DrawWireCube(point, new Vector3(1.0f, 1.0f, 1.0f));
        }
        */
    }

    private UniCom.Subscriber<Npc.Vehicle.LocalWaypoints> LocalWaypointsSub_;
    public string LocalWaypointTopic;
    private Npc.Vehicle.LocalWaypoints LocalWaypoints_;
}
