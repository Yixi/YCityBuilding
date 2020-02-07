using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingT : MonoBehaviour
{
    // Start is called before the first frame update
    public Waypoint topOut;
    public Waypoint topIn;
    public Waypoint leftOut;
    public Waypoint leftIn;
    public Waypoint bottomOut;
    public Waypoint bottomIn;
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0);
        Waypoint.DrawWayPointPath(new List<Waypoint>
        {
            topOut, leftIn, topIn, leftOut, bottomIn, bottomOut
        });
    }
#endif
}
