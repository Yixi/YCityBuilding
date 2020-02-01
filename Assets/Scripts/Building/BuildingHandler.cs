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
            !_buildingManager.IsHaveBuilding((Vector3)gridPosition))
        {
            _buildingManager.addBuilding(selectBuilding, (Vector3)gridPosition);
        }
    }

    void BuildingPlaceholder()
    {
        Vector3? gridPosition = GetMouseGridPosition();

        if (gridPosition != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            _placeholderBuilding.gameObject.transform.position = (Vector3) gridPosition;
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
        if (selectBuilding.type == Building.BuildingType.ROAD)
        {
            return true;
        }
        else
        {
            
        }

        return false;
    }
}