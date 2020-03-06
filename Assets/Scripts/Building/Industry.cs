using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Industry : Building
{

    [Header("Industry values")]
    public int maxCapacity;

    private DispatchCenter dispatchCenter;
   
    private float produceTimer = 2.0f;
    private int currentCapacity = 0;

    private void Start()
    {
        dispatchCenter = GameObject.Find("Game Manager").GetComponent<DispatchCenter>();
    }

    private void Update()
    {
        if (isActive)
        {
            produce();
        }
        if (currentCapacity > 5 && dispatchCenter.receiptBusinesses.Count > 0) {
            dispatchCenter.ShipFromIndustry(this);
        }
    }

    public void ReadyToProduce()
    {
        isActive = true;
    }

    public void OutBound(int count) {
        currentCapacity -= count;
    }

    private void produce()
    {
        if (currentCapacity < maxCapacity)
        {
            produceTimer -= Time.deltaTime;
            if (produceTimer <= 0)
            {
                currentCapacity += 1;
                produceTimer = 2.0f;
            }
        }
    }

}
