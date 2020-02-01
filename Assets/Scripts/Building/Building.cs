using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
   public enum BuildingType
   {
      Business,
      Industry,
      Uptown,
      Road,
      Tree
   }

   public enum DIRECTION
   {
      Left, Top, Right, Bottom
   }
   
   public int id;
   public int cost;
   public BuildingType type;

   public void SetDirection(DIRECTION direction)
   {
      var childGameObject = transform.GetChild(0);

      if (direction == DIRECTION.Left)
      {
         childGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
      }

      if (direction == DIRECTION.Top)
      {
         childGameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
      }

      if (direction == DIRECTION.Right)
      {
         childGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
      }

      if (direction == DIRECTION.Bottom)
      {
         childGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
      }
   }
   
}
