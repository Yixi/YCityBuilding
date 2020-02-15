using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Building building;

    public bool IsHaveBuilding()
    {
        return building && building.type != Building.BuildingType.Tree;
    }
}
