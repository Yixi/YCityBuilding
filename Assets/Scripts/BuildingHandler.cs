using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingHandler : MonoBehaviour
{
    [HideInInspector]
    public Building selectBuilding;
    private BuildingManager _buildingManager;

    [HideInInspector]
    public bool isInBuilder
    {
        get { return selectBuilding; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _buildingManager = GetComponent<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectBuilding)
        {
            InteractWithGround();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisableBuilder();
        }
    }

    public void EnableBuilder(Building building)
    {
        selectBuilding = building;
    }

    public void DisableBuilder()
    {
        selectBuilding = null;
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
}