using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uptown : Building
{

    [Header("Uptown values")]
    public int maxPeopleCount;
    private ResourceManager _resourceManager;
    private float checkInTimer = 10.0f;
    private int currentPeopleIn;

    private void Awake()
    {
        _resourceManager = GameObject.Find("Game Manager").GetComponent<ResourceManager>();
    }
    public void ReadyToCheckIn()
    {
        if (isActive)
        {
            IncreasePeople();
        }
    }

    private void IncreasePeople()
    {

        if (currentPeopleIn < maxPeopleCount)
        {
            checkInTimer -= Time.deltaTime;
            if (checkInTimer <= 0)
            {
                var increased = Random.Range(0, 3);

                currentPeopleIn += increased;
                _resourceManager.increasePeople(increased);
            }
        }


    }
}
