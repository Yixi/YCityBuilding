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

    public GameObject straight;
    public GameObject crossing;
    public GameObject crossingT;
    public GameObject turn;

    [SerializeField] private GameObject road;
    [SerializeField] private TYPE roadType = TYPE.Straight;
    [SerializeField] public int rotation = 0;
    
    
    private List<GameObject> roadsPrefs;

    public void Awake()
    {
        roadsPrefs = new List<GameObject> {straight, turn, crossingT, crossing};
        SetTypeAndDirection(roadType, rotation);
    }
    
    
    public void SetTypeAndDirection(TYPE t, float r = 0f)
    {
        rotation = (int) r;
        roadType = t;
        roadsPrefs.ForEach(p => p.SetActive(false));
        roadsPrefs[(int) roadType].SetActive(true);
        road.transform.rotation = Quaternion.Euler(new Vector3(0, r, 0));
    }

    public List<Waypoint> GetWayPoints()
    {
        Straight _straight = straight.GetComponent<Straight>();
        Crossing _crossing = crossing.GetComponent<Crossing>();
        CrossingT _crossingT = crossingT.GetComponent<CrossingT>();
        Turn _turn = turn.GetComponent<Turn>();
        switch (roadType)
        {
            case TYPE.Straight:
                return new List<Waypoint>
                {
                    _straight.topIn, _straight.topOut, null, null, _straight.bottomIn, _straight.bottomOut, null, null
                };
            case TYPE.Turn:
                return new List<Waypoint>
                {
                    _turn.topIn, _turn.topOut, null, null, null, null, _turn.leftIn, _turn.leftOut
                };
            case TYPE.CrossingT:
                return new List<Waypoint>
                {
                    _crossingT.topIn, _crossingT.topOut, null,null,
                    _crossingT.bottomIn, _crossingT.bottomOut, _crossingT.leftIn, _crossingT.leftOut
                };
            case TYPE.Crossing:
                return new List<Waypoint>
                {
                    _crossing.topIn, _crossing.topOut, _crossing.rightIn,_crossing.rightOut,
                    _crossing.bottomIn, _crossing.bottomOut, _crossing.leftIn, _crossing.leftOut
                };
        }
        return new List<Waypoint>();
    }
    

}