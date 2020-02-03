using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildingHandler : MonoBehaviour
{
    [HideInInspector] public Building selectBuilding;
    private BuildingManager _buildingManager;
    private Building _placeholderBuilding;
    private GameManager _gameManager;

    [HideInInspector]
    public bool isInBuilder
    {
        get { return selectBuilding != null; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _buildingManager = GetComponent<BuildingManager>();
        _gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectBuilding)
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
        if (_placeholderBuilding)
        {
            Destroy(_placeholderBuilding.gameObject);
        }
        _placeholderBuilding = null;
        selectBuilding = null;
    }

    void InteractWithGround()
    {
        Vector3? gridPosition = GetMouseGridPosition();
        if (gridPosition != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() &&
            !_buildingManager.IsHaveBuilding((Vector3) gridPosition))
        {

            if (selectBuilding.type == Building.BuildingType.Road)
            {
                _buildingManager.addRoad((Road) selectBuilding, gridPosition.Value);
            }
            else
            {

                var canPlace = CanPlaceBuilding(gridPosition.Value);
                if (canPlace.Item1)
                {
                        _buildingManager.addBuilding(selectBuilding, (Vector3) gridPosition, canPlace.Item2[0]);
                }
            }
        }
    }

    void BuildingPlaceholder()
    {
        Vector3? gridPosition = GetMouseGridPosition();

        if (gridPosition != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() &&
            !_buildingManager.IsHaveBuilding(gridPosition.Value))
        {
            _placeholderBuilding.gameObject.transform.position = (Vector3) gridPosition;

            var canPlace = CanPlaceBuilding(gridPosition.Value);

            if (canPlace.Item1 && selectBuilding.type != Building.BuildingType.Road)
            {
                _placeholderBuilding.SetDirection(canPlace.Item2[0]);
            }
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

    Tuple<bool, List<Building.DIRECTION>> CanPlaceBuilding(Vector3 position)
    {
        var x = (int) position.x;
        var z = (int) position.z;
        var buildings = _buildingManager._buildings;
        var availableRoadDirection = new List<Building.DIRECTION>();
        if (selectBuilding.type == Building.BuildingType.Road)
        {
            return new Tuple<bool, List<Building.DIRECTION>>(true, availableRoadDirection);
        }

        var aroundBuilding = new List<Building>();
        var roadDirections = new List<Building.DIRECTION>();

        if (x >= 0 && z >= 0 && x < _gameManager.mapWidth && z < _gameManager.mapHeight)
        {
            if (x > 0)
            {
                aroundBuilding.Add(buildings[x - 1, z]);
                roadDirections.Add(Building.DIRECTION.Left);
            }

            if (x < buildings.GetLength(0) - 1)
            {
                aroundBuilding.Add(buildings[x + 1, z]);
                roadDirections.Add(Building.DIRECTION.Right);
            }

            if (z > 0)
            {
                aroundBuilding.Add(buildings[x, z - 1]);
                roadDirections.Add(Building.DIRECTION.Bottom);
            }

            if (z < buildings.GetLength(1) - 1)
            {
                aroundBuilding.Add(buildings[x, z + 1]);
                roadDirections.Add(Building.DIRECTION.Top);
            }

            Enumerable.Range(0, aroundBuilding.Count)
                .ToList()
                .ForEach(i =>
                {
                    if (aroundBuilding[i] != null && aroundBuilding[i].type == Building.BuildingType.Road)
                    {
                        availableRoadDirection.Add(roadDirections[i]);
                    }
                });
        }

        return new Tuple<bool, List<Building.DIRECTION>>(availableRoadDirection.Count > 0, availableRoadDirection);
    }
}