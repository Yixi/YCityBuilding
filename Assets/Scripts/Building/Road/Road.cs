using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteAlways]
public class Road : Building
{
    [HideInInspector]
    public enum TYPE
    {
        Straight = 0,
        Turn = 1,
        CrossingT = 2,
        Crossing = 3,
    }

    public List<Waypoint> waypoints;
    public GameObject straight;
    public GameObject crossing;
    public GameObject crossingT;
    public GameObject turn;

    [SerializeField] private GameObject road;
    [SerializeField] private TYPE roadType = TYPE.Straight;
    [SerializeField] private float rotation = 0;
    
    
    private List<GameObject> roadsPrefs;

    public void Start()
    {
        roadsPrefs = new List<GameObject> {straight, turn, crossingT, crossing};
        SetTypeAndDirection(roadType, rotation);
    }
    
    
    public void SetTypeAndDirection(TYPE t, float r)
    {
        roadType = t;
        roadsPrefs.ForEach(p => p.SetActive(false));
        roadsPrefs[(int) roadType].SetActive(true);
        road.transform.rotation = Quaternion.Euler(new Vector3(0, r, 0));
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0);
        Gizmos.DrawLine(waypoints[1].transform.position, waypoints[0].transform.position);
        DrawArrow.ForGizmo(waypoints[1].transform.position,
            (waypoints[0].transform.position - waypoints[1].transform.position) * 0.5f);
        Gizmos.DrawLine(waypoints[2].transform.position, waypoints[3].transform.position);
        DrawArrow.ForGizmo(waypoints[2].transform.position,
            (waypoints[3].transform.position - waypoints[2].transform.position) * 0.5f);
    }
#endif

}