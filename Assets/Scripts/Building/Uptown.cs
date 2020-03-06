using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uptown : Building
{

    [Header("Uptown values")]
    public int maxPeopleCount;
    [HideInInspector] public ResourceManager resourceManager;
    private int currentPeopleIn = 0;

    public void ReadyToCheckIn()
    {
        IncreasePeople();
    }

    private void IncreasePeople() {
        var increased = Random.Range(0, 3);

        currentPeopleIn += increased;
        resourceManager.increasePeople(increased);        

        if (currentPeopleIn < maxPeopleCount) {
            Invoke("IncreasePeople", 10.0f);
        }
    }
}
