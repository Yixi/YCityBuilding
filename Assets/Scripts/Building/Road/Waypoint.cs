using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint: MonoBehaviour {
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.98f, 1f, 0f, 0.7f);
        Gizmos.DrawSphere(transform.position, 0.05f);
    }
}
