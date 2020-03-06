using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Business : Building {    // Start is called before the first frame update
    [Header("Business values")]
    public int maxCapacity;

    private int currentCapacity = 0;
}
