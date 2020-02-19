using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Tile[,] tiles;

    public GameObject buildingParent;
    public GameObject roadsParent;
    public VehicleController vehicleController;

    [SerializeField] private ParticleSystem place;
    private GameManager _gameManager;

    private Ground _ground;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _ground = GameObject.Find("Ground").GetComponent<Ground>();

        InitTiles();

        _ground.InitTrees();
    }

    private void InitTiles()
    {
        tiles = new Tile[_gameManager.mapWidth, _gameManager.mapHeight];
        for (int i = 0; i < _gameManager.mapWidth; i++)
        {
            for (int j = 0; j < _gameManager.mapHeight; j++)
            {
                tiles[i, j] = new Tile();
            }
        }
    }

    // Update is called once per frame

    public void addBuilding(Building building, Vector3 position,
        Building.DIRECTION direction = Building.DIRECTION.Bottom)
    {
        DestoryExistNature(position);

        var addedBuilding = Instantiate(building, position, Quaternion.identity, buildingParent.transform);
        addedBuilding.SetDirection(direction);
        Instantiate(place, position, Quaternion.identity, buildingParent.transform);

        tiles[(int) position.x, (int) position.z].building = addedBuilding;

        var width = building.width;
        var height = building.height;

        IEnumerable<int> xRange = Enumerable.Range(0, 1);
        IEnumerable<int> zRange = Enumerable.Range(0, 1);

        if (direction == Building.DIRECTION.Top)
        {
            xRange = Enumerable.Range(0, width);
            zRange = Enumerable.Range(0, height);
        }

        if (direction == Building.DIRECTION.Right)
        {
            xRange = Enumerable.Range(0, height);
            zRange = Enumerable.Range(-width + 1, width);
        }

        if (direction == Building.DIRECTION.Bottom)
        {
            xRange = Enumerable.Range(-width + 1, width);
            zRange = Enumerable.Range(-height + 1, height);
        }

        if (direction == Building.DIRECTION.Left)
        {
            xRange = Enumerable.Range(-height + 1, height);
            zRange = Enumerable.Range(0, width);
        }

        xRange.ToList().ForEach(x =>
        {
            zRange.ToList().ForEach(z =>
            {
                DestoryExistNature(new Vector3(position.x + x, 0, position.z + z));
                tiles[(int) position.x + x, (int) position.z + z].referenceBuilding = addedBuilding;
            });
        });
    }


    private void DestoryExistNature(Vector3 position)
    {
        var x = (int) position.x;
        var z = (int) position.z;
        var build = tiles[x, z].building;
        if (build && build.type == Building.BuildingType.Tree)
        {
            Destroy(build.gameObject);
            tiles[x, z].building = null;
        }
    }

    public bool IsHaveBuilding(Vector3 position)
    {
        if (position.x < 0 || position.x >= _gameManager.mapWidth || position.z < 0 ||
            position.z >= _gameManager.mapHeight)
        {
            return true;
        }

        return tiles[(int) position.x, (int) position.z].IsHasBuilding();
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        return new Vector3(Mathf.Floor(position.x), 0, Mathf.Floor(position.z));
    }

    public void addRoad(Road road, Vector3 position)
    {
        var x = (int) position.x;
        var z = (int) position.z;

        DestoryExistNature(position);

        var addedRoad = Instantiate(road, position, Quaternion.identity, roadsParent.transform);
        tiles[x, z].building = addedRoad;

        CorrectionRoad(x, z, true);
        ConnectRoadPath(x, z, true);

        if (roadsParent.GetComponentsInChildren<Road>().Length % 5 == 0)
        {
            vehicleController.AddAutoDriveCar();
        }
    }

    private void CorrectionRoad(int x, int z, bool needFixAround = false)
    {
        if (x < 0 || z < 0 || x >= _gameManager.mapWidth || z >= _gameManager.mapWidth) return;

        var building = tiles[x, z].building;

        if (building?.type != Building.BuildingType.Road) return;

        var road = (Road) building;

        var around = new List<Building>();

        if (x < _gameManager.mapWidth - 1) around.Add((tiles[x + 1, z].building));
        if (z > 0) around.Add(tiles[x, z - 1].building);
        if (x > 0) around.Add(tiles[x - 1, z].building);
        if (z < _gameManager.mapHeight - 1) around.Add(tiles[x, z + 1].building);

        var TypeRoad = Building.BuildingType.Road;
        var aroundRoad = around.FindAll((r => r != null && r.type == TypeRoad));
        var aroundRoadCount = aroundRoad.Count;


        if (aroundRoadCount == 4)
        {
            road.SetTypeAndDirection(Road.TYPE.Crossing);
        }

        if (aroundRoadCount == 3)
        {
            if (around[0]?.type == TypeRoad && around[1]?.type == TypeRoad && around[2]?.type == TypeRoad)
            {
                road.SetTypeAndDirection(Road.TYPE.CrossingT, 270);
            }

            if (around[1]?.type == TypeRoad && around[2]?.type == TypeRoad && around[3]?.type == TypeRoad)
            {
                road.SetTypeAndDirection(Road.TYPE.CrossingT, 0);
            }

            if (around[2]?.type == TypeRoad && around[3]?.type == TypeRoad && around[0]?.type == TypeRoad)
            {
                road.SetTypeAndDirection(Road.TYPE.CrossingT, 90);
            }

            if (around[3]?.type == TypeRoad && around[0]?.type == TypeRoad && around[1]?.type == TypeRoad)
            {
                road.SetTypeAndDirection(Road.TYPE.CrossingT, 180);
            }
        }

        if (aroundRoadCount == 2)
        {
            if (around[0]?.type == TypeRoad && around[2]?.type == TypeRoad)
            {
                road.SetTypeAndDirection(Road.TYPE.Straight, 90);
            }
            else if (around[1]?.type == TypeRoad && around[3]?.type == TypeRoad)
            {
                road.SetTypeAndDirection(Road.TYPE.Straight);
            }
            else
            {
                if (around[0]?.type == TypeRoad && around[1]?.type == TypeRoad)
                {
                    road.SetTypeAndDirection(Road.TYPE.Turn, 180);
                }

                if (around[0]?.type == TypeRoad && around[3]?.type == TypeRoad)
                {
                    road.SetTypeAndDirection(Road.TYPE.Turn, 90);
                }

                if (around[1]?.type == TypeRoad && around[2]?.type == TypeRoad)
                {
                    road.SetTypeAndDirection(Road.TYPE.Turn, 270);
                }

                if (around[2]?.type == TypeRoad && around[3]?.type == TypeRoad)
                {
                    road.SetTypeAndDirection(Road.TYPE.Turn, 0);
                }
            }
        }

        if (aroundRoadCount == 1)
        {
            if (around[0]?.type == TypeRoad || around[2]?.type == TypeRoad)
            {
                road.SetTypeAndDirection(Road.TYPE.Straight, 90);
            }
            else
            {
                road.SetTypeAndDirection(Road.TYPE.Straight);
            }
        }

        if (needFixAround)
        {
            CorrectionRoad(x + 1, z);
            CorrectionRoad(x, z - 1);
            CorrectionRoad(x - 1, z);
            CorrectionRoad(x, z + 1);
        }
    }

    private void ConnectRoadPath(int x, int z, bool needFixAround = false)
    {
        if (x < 0 || z < 0 || x >= _gameManager.mapWidth || z >= _gameManager.mapWidth) return;
        var building = tiles[x, z].building;
        if (building?.type != Building.BuildingType.Road) return;

        if (z < _gameManager.mapHeight - 1 && tiles[x, z + 1].building?.type == Building.BuildingType.Road)
        {
            ConnectPathBetweenRoad((Road) building, (Road) tiles[x, z + 1].building, 0);
        }

        if (x < _gameManager.mapWidth - 1 && tiles[x + 1, z].building?.type == Building.BuildingType.Road)
        {
            ConnectPathBetweenRoad((Road) building, (Road) tiles[x + 1, z].building, 2);
        }

        if (z > 0 && tiles[x, z - 1].building?.type == Building.BuildingType.Road)
        {
            ConnectPathBetweenRoad((Road) building, (Road) tiles[x, z - 1].building, 4);
        }

        if (x > 0 && tiles[x - 1, z].building?.type == Building.BuildingType.Road)
        {
            ConnectPathBetweenRoad((Road) building, (Road) tiles[x - 1, z].building, 6);
        }

        if (needFixAround)
        {
            ConnectRoadPath(x + 1, z);
            ConnectRoadPath(x, z - 1);
            ConnectRoadPath(x - 1, z);
            ConnectRoadPath(x, z + 1);
        }
    }

    private void ConnectPathBetweenRoad(Road roadA, Road roadB, int offset)
    {
        int calculateIndex(int index)
        {
            if (index < 0) return index + 8;
            if (index >= 8) return index - 8;
            return index;
        }

        var roadAWaypoins = roadA.GetWayPoints();
        var roadBWaypoins = roadB.GetWayPoints();
        var roadAIndexOffset = -(roadA.rotation / 90) * 2 + offset;
        var roadBIndexOffset = -(roadB.rotation / 90) * 2 + offset;

        roadAWaypoins[calculateIndex(1 + roadAIndexOffset)].nextWaypoints = new List<Waypoint>
            {roadBWaypoins[calculateIndex(4 + roadBIndexOffset)]};
        roadBWaypoins[calculateIndex(5 + roadBIndexOffset)].nextWaypoints = new List<Waypoint>
            {roadAWaypoins[calculateIndex(0 + roadAIndexOffset)]};

        if (roadA.roadType == Road.TYPE.Straight)
        {
            if (roadAWaypoins[1].nextWaypoints.Count == 0)
            {
                roadAWaypoins[1].nextWaypoints = new List<Waypoint> {roadAWaypoins[0]};
            }

            if (roadAWaypoins[5].nextWaypoints.Count == 0)
            {
                roadAWaypoins[5].nextWaypoints = new List<Waypoint> {roadAWaypoins[4]};
            }
        }
    }
}