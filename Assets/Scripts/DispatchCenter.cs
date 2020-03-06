using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DispatchCenter : MonoBehaviour
{

    // [HideInInspector]
    public List<Business> receiptBusinesses;
    private List<Industry> shipIndustrys;
    private List<Uptown> buyUptowns;

    public void AddToReceiptBusinesses(Business business)
    {
        if (!receiptBusinesses.Contains(business))
        {
            receiptBusinesses.Add(business);
        }
    }

    public void RemoveReceiptBusinesses(Business business)
    {
        if (receiptBusinesses.Contains(business))
        {
            receiptBusinesses.Remove(business);
        }
    }

    public void ShipFromIndustry(Industry industry)
    {
        var bs = receiptBusinesses.OrderBy(b => b.logicCapacity).ToArray();
        var needShipBusiness = bs[0];

        needShipBusiness.addShiping(5);
        industry.OutBound(5);
    }

}
