using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Industry : Building
{
    
    [Header("Industry values")]
    public int maxCapacity;
    
    
    private int currentCapacity = 0;


    public void ReadyToProduce() {
        Invoke("produce", 2.0f);
    }

    private void produce() {
        currentCapacity += 1;
        if (currentCapacity < maxCapacity) {
            Invoke("produce", 2.0f);
        }
    }

}
