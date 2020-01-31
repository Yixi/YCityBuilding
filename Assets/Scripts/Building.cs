using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
   public enum BuildingType
   {
      BUSINESS,
      INDUSTRY,
      UPTOWN,
      ROAD,
   }
   public int id;
   public int cost;
   public BuildingType type;
}
