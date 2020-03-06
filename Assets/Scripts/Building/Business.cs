using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Business : Building
{    // Start is called before the first frame update
    [Header("Business values")]
    public int maxCapacity;

    private DispatchCenter dispatchCenter;
    private int currentCapacity = 0;
    private int shipingCapacity = 0;
    private bool isInDispaterCenter = false;

    public int logicCapacity
    {
        get
        {
            return currentCapacity + shipingCapacity;
        }
    }
    private void Start()
    {
        dispatchCenter = GameObject.Find("Game Manager").GetComponent<DispatchCenter>();
    }

    private void Update()
    {
        if (isActive) {
            ManageReceipt();
        }
    }

    public void addShiping(int count) {
        shipingCapacity += count;
    }

    private void ManageReceipt()
    {
        if (currentCapacity < 5)
        {
            if (!isInDispaterCenter)
            {
                addToDispatchCenter();
            }
        }

        if (maxCapacity - logicCapacity < 5)
        {
            if (isInDispaterCenter)
            {
                removeFromDispatchCenter();
            }
        }
    }

    private void addToDispatchCenter()
    {
        dispatchCenter.AddToReceiptBusinesses(this);
        isInDispaterCenter = true;
    }

    private void removeFromDispatchCenter()
    {
        dispatchCenter.RemoveReceiptBusinesses(this);
        isInDispaterCenter = false;
    }
}
