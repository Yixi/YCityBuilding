using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public enum ROAD_DIRECTION
    {
        LEFT,
        TOP,
        RIGHT,
        BOTTOM
    }

    public Hashtable ROAD_DIRECTION_HASH = new Hashtable()
    {
        {ROAD_DIRECTION.LEFT, new Vector3(0, -90, 0)},
        {ROAD_DIRECTION.TOP, Vector3.zero},
        {ROAD_DIRECTION.RIGHT, new Vector3(0, 90, 0)},
        {ROAD_DIRECTION.BOTTOM, new Vector3(0, 180, 0)}
    };

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

    public void addBuilding(Building building, Vector3 position, ROAD_DIRECTION direction = ROAD_DIRECTION.BOTTOM)
    {
        var build = _buildings[(int) position.x, (int) position.z];
        if (build && build.type == Building.BuildingType.TREE)
        {
            Destroy(build.gameObject);
        }

        var addedBuilding = Instantiate(building, position, Quaternion.identity, buildingParent.transform);
        addedBuilding.transform.GetChild(0).transform.rotation = Quaternion.Euler((Vector3) ROAD_DIRECTION_HASH[direction]);
        _buildings[(int) position.x, (int) position.z] = addedBuilding;
        
        if (building.type != Building.BuildingType.ROAD)
        {
            Instantiate(place, position, Quaternion.identity, buildingParent.transform);
        }
    }

    public bool IsHaveBuilding(Vector3 position)
    {
        return _buildings[(int) position.x, (int) position.z] &&
               _buildings[(int) position.x, (int) position.z].type != Building.BuildingType.TREE;
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        return new Vector3(Mathf.Floor(position.x), 0, Mathf.Floor(position.z));
    }
}