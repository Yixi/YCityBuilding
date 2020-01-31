using System;
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
      TREE
   }
   public int id;
   public int cost;
   public BuildingType type;
}
