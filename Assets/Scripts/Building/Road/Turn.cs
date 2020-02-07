using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public Waypoint topOut;
    public Waypoint leftIn;
    public Waypoint topIn;
    public Waypoint leftOut;
    public Waypoint middle;
    // Start is called before the first frame update

        
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0);
        Waypoint.DrawWayPointPath(new List<Waypoint>
        {
            topOut, leftIn, topIn, leftOut, middle
        });
    }
#endif
}
