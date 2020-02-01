using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class BuildingHandler : MonoBehaviour
{
    [HideInInspector] public Building selectBuilding;
    private BuildingManager _buildingManager;
    private Building _placeholderBuilding;

    [HideInInspector]
    public bool isInBuilder
    {
        get { return selectBuilding != null; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _buildingManager = GetComponent<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectBuilding != null)
        {
            InteractWithGround();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            DisableBuilder();
        }

        if (isInBuilder)
        {
            BuildingPlaceholder();
        }
    }

    public void EnableBuilder(Building building)
    {
        if (_placeholderBuilding != null)
        {
            Destroy(_placeholderBuilding.gameObject);
        }

        selectBuilding = building;

        _placeholderBuilding = Instantiate(building, new Vector3(), building.transform.rotation);
        // _placeholderBuilding.gameObject.SetActive(false);
    }

    public void DisableBuilder()
    {
        selectBuilding = null;
        Destroy(_placeholderBuilding.gameObject);
        _placeholderBuilding = null;
    }

    void InteractWithGround()
    {
        Vector3? gridPosition = GetMouseGridPosition();
        if (gridPosition != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() &&
            !_buildingManager.IsHaveBuilding((Vector3) gridPosition))
        {
            if (CanPlaceBuilding((Vector3) gridPosition))
            {
                _buildingManager.addBuilding(selectBuilding, (Vector3) gridPosition);
            }
        }
    }

    void BuildingPlaceholder()
    {
        Vector3? gridPosition = GetMouseGridPosition();

        if (gridPosition != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            // if (CanPlaceBuilding((Vector3) gridPosition) && !_buildingManager.IsHaveBuilding((Vector3) gridPosition))
            // {
            //     _placeholderBuilding.gameObject.SetActive(true);
            _placeholderBuilding.gameObject.transform.position = (Vector3) gridPosition;
            // }
            // else
            // {
            // _placeholderBuilding.gameObject.SetActive(false);
            // }
        }
    }

    Vector3? GetMouseGridPosition()
    {
        Plane plane = new Plane(Vector3.up, new Vector3(0, 0.5f, 0));
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float entry;
        if (plane.Raycast(ray, out entry))
        {
            return _buildingManager.CalculateGridPosition(ray.GetPoint(entry));
        }

        return null;
    }

    bool CanPlaceBuilding(Vector3 position)
    {
        var x = (int) position.x;
        var z = (int) position.z;
        var buildings = _buildingManager._buildings;
        if (selectBuilding.type == Building.BuildingType.ROAD)
        {
            return true;
        }
        else
        {
            List<Building> aroundBuilding = new List<Building>();
            if (x > 0)
            {
                aroundBuilding.Add(buildings[x - 1, z]);
            }

            if (x < buildings.GetLength(0))
            {
                aroundBuilding.Add(buildings[x + 1, z]);
            }

            if (z > 0)
            {
                aroundBuilding.Add(buildings[x, z - 1]);
            }

            if (z < buildings.GetLength(1))
            {
                aroundBuilding.Add(buildings[x, z + 1]);
            }

            Debug.Log(aroundBuilding);

            if (aroundBuilding.FindAll(b => b != null && b.type == Building.BuildingType.ROAD).Count > 0)
            {
                return true;
            }
        }

        return false;
    }
}