using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : MonoBehaviour
{

    public Waypoint bottomIn;
    public Waypoint bottomOut;
    public Waypoint topIn;
    public Waypoint topOut;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0);
        Waypoint.DrawWayPointPath(new List<Waypoint>{bottomIn, bottomOut, topIn, topOut});
    }
#endif
    
}
