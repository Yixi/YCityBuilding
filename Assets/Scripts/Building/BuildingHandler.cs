using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class BuildingHandler : MonoBehaviour
{
    [HideInInspector]
    public Building selectBuilding;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var gridPosition = _buildingManager.CalculateGridPosition(hit.point);
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() &&
                !_buildingManager.IsHaveBuilding(gridPosition))
            {
                _buildingManager.addBuilding(selectBuilding, gridPosition);
            }
        }
    }

    void BuildingPlaceholder()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var gridPosition = _buildingManager.CalculateGridPosition(hit.point);
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                _placeholderBuilding.gameObject.transform.position = gridPosition;
            }
        }
    }
}