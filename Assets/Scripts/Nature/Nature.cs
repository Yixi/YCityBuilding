using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nature : Building
{
    // Start is called before the first frame update

    public void SetRandomRotation()
    {
        transform.GetChild(0).transform.rotation = Quaternion.Euler(0, Random.Range(0, 12) * 30, 0);
    }
}
