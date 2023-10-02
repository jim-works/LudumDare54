using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderWaypointManager : MonoBehaviour
{
    public static WanderWaypointManager Singleton;
    [SerializeField]
    private WanderWaypointManager secondary;
    public float PrimaryMinX = 0;
    public bool Secondary = false;
    private WanderWaypoint[] waypoints;

    void Awake()
    {
        waypoints = GetComponentsInChildren<WanderWaypoint>();
        if (!Secondary) {
            Singleton = this;
        }
    }

    public WanderWaypoint RandomWaypoint(bool secondary)
    {
        if (secondary)
        {
            return this.secondary.RandomWaypoint(false);
        }
        else
        {
            return waypoints[Random.Range(0,waypoints.Length)];
        }
    }

    public Vector2 GetWanderDestination(Vector2 position)
    {
        WanderWaypoint waypoint = RandomWaypoint(position.x < PrimaryMinX);
        return (Vector2)waypoint.transform.position + Random.insideUnitCircle*waypoint.Radius;
    }
}
