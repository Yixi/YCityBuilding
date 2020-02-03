using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Building[,] _buildings;
    public GameObject buildingParent;

    [SerializeField] private ParticleSystem place;
    private GameManager _gameManager;

    private Ground _ground;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _ground = GameObject.Find("Ground").GetComponent<Ground>();
        _buildings = new Building[_gameManager.mapWidth, _gameManager.mapHeight];
        _ground.InitTrees();
    }

    // Update is called once per frame

    public void addBuilding(Building building, Vector3 position,
        Building.DIRECTION direction = Building.DIRECTION.Bottom)
    {
        DestoryExistNature(position);

        var addedBuilding = Instantiate(building, position, Quaternion.identity, buildingParent.transform);
        addedBuilding.SetDirection(direction);
        _buildings[(int) position.x, (int) position.z] = addedBuilding;
        Instantiate(place, position, Quaternion.identity, buildingParent.transform);
    }

    public void addRoad(Road road, Vector3 position)
    {
        var x = (int) position.x;
        var z = (int) position.z;

        DestoryExistNature(position);

        var addedRoad = Instantiate(road, position, Quaternion.identity);
        _buildings[(int) position.x, (int) position.z] = addedRoad;

        var around = new List<Building>();

        if (x < _gameManager.mapWidth - 1) around.Add((_buildings[x + 1, z]));
        if (z > 0) around.Add(_buildings[x, z - 1]);
        if (x > 0) around.Add(_buildings[x - 1, z]);
        if (z < _gameManager.mapHeight - 1) around.Add(_buildings[x, z + 1]);

        around = around.FindAll((r => r && r.type == Building.BuildingType.Road));

        if (around.Count == 4)
        {
            addedRoad.SetTypeAndDirection(Road.TYPE.Crossing);
        }

        if (around.Count == 3)
        {
            addedRoad.SetTypeAndDirection(Road.TYPE.CrossingT);
        }

        if (around.Count == 2)
        {
            addedRoad.SetTypeAndDirection(Road.TYPE.Turn);
        }

        if (around.Count == 1)
        {
            addedRoad.SetTypeAndDirection(Road.TYPE.Straight);
        }

    }

    private void DestoryExistNature(Vector3 position)
    {
        var build = _buildings[(int) position.x, (int) position.z];
        if (build && build.type == Building.BuildingType.Tree)
        {
            Destroy(build.gameObject);
        }
    }

    public bool IsHaveBuilding(Vector3 position)
    {
        if (position.x < 0 || position.x >= _gameManager.mapWidth || position.z < 0 ||
            position.z >= _gameManager.mapHeight)
        {
            return true;
        }

        return _buildings[(int) position.x, (int) position.z] &&
               _buildings[(int) position.x, (int) position.z].type != Building.BuildingType.Tree;
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        return new Vector3(Mathf.Floor(position.x), 0, Mathf.Floor(position.z));
    }
}