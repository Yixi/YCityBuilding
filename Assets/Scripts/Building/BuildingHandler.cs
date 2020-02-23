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
        var gridPosition = GetMouseGridPosition();
        if (gridPosition != null &&
            !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() &&
            CanPlaceBuilding(gridPosition.Value)
        )
        {
            if (selectBuilding.type == Building.BuildingType.Road)
            {
                _buildingManager.addRoad((Road) selectBuilding, gridPosition.Value);
            }
            else
            {
                _buildingManager.addBuilding(
                    selectBuilding,
                    gridPosition.Value,
                    _placeholderBuilding.Direction
                );
            }
        }
    }

    void BuildingPlaceholder()
    {
        var gridPosition = GetMouseGridPosition();

        if (Input.GetKeyDown(KeyCode.R))
        {
            _placeholderBuilding.Rotate();
        }

        if (gridPosition != null)
        {
            _placeholderBuilding.gameObject.transform.position = gridPosition.Value;
            if (CanPlaceBuilding(gridPosition.Value))
            {
                _placeholderBuilding.SetColor(new Color(0, 1, 0, .5f));
            }
            else
            {
                _placeholderBuilding.SetColor(new Color(1, 0, 0, .5f));
            }
        }
    }

    Boolean CanPlaceBuilding(Vector3 position)
    {
        try
        {
            var x = (int) position.x;
            var z = (int) position.z;
            var buildingWidth = _placeholderBuilding.width;
            var buildingHeight = _placeholderBuilding.height;
            var direction = _placeholderBuilding.Direction;
            var tiles = _buildingManager.tiles;
            var currentTile = tiles[x, z];

            if (selectBuilding.type == Building.BuildingType.Road && !currentTile.IsHasBuilding())
            {
                return true;
            }

            // 1. make all tile have no building; 2. the direction must near the road;
            if (direction == Building.DIRECTION.Top)
            {
                for (var i = 0; i < buildingWidth; i++)
                {
                    for (var j = 0; j < buildingHeight; j++)
                    {
                        if (tiles[x + i, z + j].IsHasBuilding()) return false;
                    }
                }

                var topRoadCount = Enumerable.Range(0, buildingWidth)
                    .ToList()
                    .FindAll(i => tiles[x + i, z + buildingHeight].building?.type == Building.BuildingType.Road).Count;

                return topRoadCount != 0;
            }

            if (direction == Building.DIRECTION.Right)
            {
                for (var i = 0; i < buildingHeight; i++)
                {
                    for (var j = 0; j > -buildingWidth; j--)
                    {
                        if (tiles[x + i, z + j].IsHasBuilding()) return false;
                    }
                }

                var rightRoadCount = Enumerable.Range(0, buildingWidth)
                    .ToList()
                    .FindAll(i => tiles[x + buildingHeight, z - i].building?.type == Building.BuildingType.Road).Count;
                return rightRoadCount != 0;
            }

            if (direction == Building.DIRECTION.Bottom)
            {
                for (var i = 0; i > -buildingWidth; i--)
                {
                    for (var j = 0; j > -buildingHeight; j--)
                    {
                        if (tiles[x + i, z + j].IsHasBuilding()) return false;
                    }
                }

                var bottomRoadCount = Enumerable.Range(0, buildingWidth)
                    .ToList()
                    .FindAll(i => tiles[x - i, z - buildingHeight].building?.type == Building.BuildingType.Road).Count;
                return bottomRoadCount != 0;
            }

            if (direction == Building.DIRECTION.Left)
            {
                for (var i = 0; i > -buildingHeight; i--)
                {
                    for (var j = 0; j < buildingWidth; j++)
                    {
                        if (tiles[x + i, z + j].IsHasBuilding()) return false;
                    }
                }

                var leftRoadCount = Enumerable.Range(0, buildingWidth)
                    .ToList()
                    .FindAll(i => tiles[x - buildingHeight, z + i].building?.type == Building.BuildingType.Road).Count;
                return leftRoadCount != 0;
            }
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }

        return false;
    }

    Vector3? GetMouseGridPosition()
    {
        var plane = new Plane(Vector3.up, new Vector3(0, 0.5f, 0));
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float entry;
        if (plane.Raycast(ray, out entry))
        {
            return _buildingManager.CalculateGridPosition(ray.GetPoint(entry));
        }

        return null;
    }
}