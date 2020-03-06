using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispatchCenter : MonoBehaviour
{
    private List<Business> receiptBusinesses;
    private List<Industry> shipIndustrys;
    private List<Uptown> buyUptowns;

    public void AddToReceiptBusinesses(Business business)
    {
        if (!receiptBusinesses.Contains(business))
        {
            receiptBusinesses.Add(business);
        }
    }

}
