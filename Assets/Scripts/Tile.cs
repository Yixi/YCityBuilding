using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    //the building Instantiate tile
    public Building building;

    //the building reference tile
    public Building referenceBuilding;

    public bool IsHasBuilding()
    {
        return (building || referenceBuilding) && building?.type != Building.BuildingType.Tree;
    }
}