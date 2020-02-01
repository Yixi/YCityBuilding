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
            var canPlace = CanPlaceBuilding(gridPosition.Value);
            if (canPlace.Item1)
            {
                if (canPlace.Item2.Count > 0)
                {
                    _buildingManager.addBuilding(selectBuilding, (Vector3) gridPosition, canPlace.Item2[Random.Range(0, canPlace.Item2.Count)]);
                }
                else
                {
                    _buildingManager.addBuilding(selectBuilding, (Vector3) gridPosition);
                }
                
            }
        }
    }

    void BuildingPlaceholder()
    {
        Vector3? gridPosition = GetMouseGridPosition();

        if (gridPosition != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !_buildingManager.IsHaveBuilding(gridPosition.Value))
        {
            _placeholderBuilding.gameObject.transform.position = (Vector3) gridPosition;
            var render = _placeholderBuilding.gameObject.GetComponentsInChildren<Renderer>();
            foreach (var r in render)
            {
                var color = r.material.color;
                color.a = 0.5f;
                r.material.color = color;
            }

            var canPlace = CanPlaceBuilding(gridPosition.Value);
            
            if (canPlace.Item1 && selectBuilding.type != Building.BuildingType.ROAD)
            {
                var rotation = (Vector3) _buildingManager.ROAD_DIRECTION_HASH[canPlace.Item2[0]];
                _placeholderBuilding.gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(rotation);
                
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

    Tuple<bool, List<BuildingManager.ROAD_DIRECTION>> CanPlaceBuilding(Vector3 position)
    {
        var x = (int) position.x;
        var z = (int) position.z;
        var buildings = _buildingManager._buildings;
        var availableRoadDirection = new List<BuildingManager.ROAD_DIRECTION>();
        if (selectBuilding.type == Building.BuildingType.ROAD)
        {
            return new Tuple<bool, List<BuildingManager.ROAD_DIRECTION>>(true, availableRoadDirection);
        }

        var aroundBuilding = new List<Building>();
        var roadDirections = new List<BuildingManager.ROAD_DIRECTION>();
        if (x > 0)
        {
            aroundBuilding.Add(buildings[x - 1, z]);
            roadDirections.Add(BuildingManager.ROAD_DIRECTION.LEFT);
        }

        if (x < buildings.GetLength(0))
        {
            aroundBuilding.Add(buildings[x + 1, z]);
            roadDirections.Add(BuildingManager.ROAD_DIRECTION.RIGHT);
        }

        if (z > 0)
        {
            aroundBuilding.Add(buildings[x, z - 1]);
            roadDirections.Add(BuildingManager.ROAD_DIRECTION.BOTTOM);
        }

        if (z < buildings.GetLength(1))
        {
            aroundBuilding.Add(buildings[x, z + 1]);
            roadDirections.Add(BuildingManager.ROAD_DIRECTION.TOP);
        }

        Enumerable.Range(0, aroundBuilding.Count)
            .ToList()
            .ForEach(i =>
            {
                if (aroundBuilding[i] != null && aroundBuilding[i].type == Building.BuildingType.ROAD)
                {
                    availableRoadDirection.Add(roadDirections[i]);
                }
            });

        return new Tuple<bool, List<BuildingManager.ROAD_DIRECTION>>(availableRoadDirection.Count > 0, availableRoadDirection);
    }
}