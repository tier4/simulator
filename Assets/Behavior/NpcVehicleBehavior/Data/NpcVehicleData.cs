using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simulator.Map;
using Simulator.Utilities;
using System;

namespace Npc.Vehicle
{
    /*
    class LocalWaypoint
    {
        public Pose point;
        public float maximum_speed = 0.0f;
        public float minimum_speed = 0.0f;
    }
    */

    class LocalWaypoints
    {
        public CatmullRomSpline spline;
        //public List<LocalWaypoint> waypoints; 
    }

    class GlobalWaypoint
    {
        public Vector3 point;
        public float maximum_speed = 0.0f;
        public float minimum_speed = 0.0f;
    }

    class GlobalWaypoints
    {
        public List<GlobalWaypoint> waypoints = new List<GlobalWaypoint>();
        public List<MapLane> lanes = new List<MapLane>();
    }

    class CurrentTask
    {
        public CurrentTask(Tasks task)
        {
            this.task = task;
        }

        public enum Tasks : byte
        {
            RouteSearch,
            FollowLane,
            StoppingAtStopLine,
            StoppedAtStopLine
        }

        public Tasks task;
    }
}