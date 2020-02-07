using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint: MonoBehaviour
{
    public List<Waypoint> nextWaypoints = new List<Waypoint>();
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.98f, 1f, 0f, 0.7f);
        Gizmos.DrawSphere(transform.position, 0.05f);
    }

    public static void DrawWayPointPath(List<Waypoint> waypoints)
    {
        waypoints.ForEach(waypoint =>
        {
            if (waypoint)
            {
                waypoint.nextWaypoints.ForEach(nextWayPoint =>
                {
                    if (nextWayPoint)
                    {
                        DrawPath.ForGizmo(waypoint.transform.position, nextWayPoint.transform.position);
                    }
                });
            }
        });
    }
}
