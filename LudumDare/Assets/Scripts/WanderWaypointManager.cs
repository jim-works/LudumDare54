using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderWaypointManager : MonoBehaviour
{
    public static WanderWaypointManager Singleton;
    private WanderWaypoint[] waypoints;

    void Awake()
    {
        waypoints = GetComponentsInChildren<WanderWaypoint>();
        Singleton = this;
    }

    public WanderWaypoint RandomWaypoint()
    {
        return waypoints[Random.Range(0,waypoints.Length)];
    }

    public Vector2 GetWanderDestination()
    {
        WanderWaypoint waypoint = RandomWaypoint();
        return (Vector2)waypoint.transform.position + Random.insideUnitCircle*waypoint.Radius;
    }
}
