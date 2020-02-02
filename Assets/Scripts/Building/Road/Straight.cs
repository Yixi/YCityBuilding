using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : MonoBehaviour
{
    
    public List<Waypoint> waypoints; 
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0);
        DrawPath.ForGizmo(waypoints[1].transform.position, waypoints[0].transform.position);
        DrawPath.ForGizmo(waypoints[2].transform.position, waypoints[3].transform.position);
    }
#endif
    
}
