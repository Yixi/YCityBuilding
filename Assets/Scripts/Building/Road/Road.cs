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
    [SerializeField] private float rotation = 0;
    
    
    private List<GameObject> roadsPrefs;

    public void Awake()
    {
        roadsPrefs = new List<GameObject> {straight, turn, crossingT, crossing};
        SetTypeAndDirection(roadType, rotation);
    }
    
    
    public void SetTypeAndDirection(TYPE t, float r = 0f)
    {
        roadType = t;
        roadsPrefs.ForEach(p => p.SetActive(false));
        roadsPrefs[(int) roadType].SetActive(true);
        road.transform.rotation = Quaternion.Euler(new Vector3(0, r, 0));
    }

}