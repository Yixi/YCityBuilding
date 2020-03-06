using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uptown : Building
{

    [Header("Uptown values")]
    public int maxPeopleCount;
    private ResourceManager _resourceManager;
    private int currentPeopleIn;

    private void Awake() {
        _resourceManager = GameObject.Find("Game Manager").GetComponent<ResourceManager>();
    }
    public void ReadyToCheckIn()
    {
        IncreasePeople();
    }

    private void IncreasePeople() {
        var increased = Random.Range(0, 3);

        currentPeopleIn += increased;
        _resourceManager.increasePeople(increased);        

        if (currentPeopleIn < maxPeopleCount) {
            Invoke("IncreasePeople", 10.0f);
        }
    }
}
