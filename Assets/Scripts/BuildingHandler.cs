using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    private Building _selectBuilding;
    private BuildingManager _buildingManager;

    // Start is called before the first frame update
    void Start()
    {
        _buildingManager = GetComponent<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _selectBuilding)
        {
            InteractWithGround();
        }
    }

    public void EnableBuilder(Building building)
    {
        _selectBuilding = building;
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
                _buildingManager.addBuilding(_selectBuilding, gridPosition);
            }
        }
    }
}