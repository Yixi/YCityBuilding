using System.Collections;
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
    void Update()
    {
    }

    public void addBuilding(Building building, Vector3 position)
    {
        var build = _buildings[(int) position.x, (int) position.z];
        if (build && build.type == Building.BuildingType.TREE)
        {
            Destroy(build.gameObject);
        }
        _buildings[(int) position.x, (int) position.z] =
            Instantiate(building, position, Quaternion.identity, buildingParent.transform);
        if (building.type != Building.BuildingType.ROAD)
        {
            Instantiate(place, position, Quaternion.identity, buildingParent.transform);
        }
    }

    public bool IsHaveBuilding(Vector3 position)
    {
        return _buildings[(int) position.x, (int) position.z] && _buildings[(int) position.x, (int) position.z].type != Building.BuildingType.TREE;
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        return new Vector3(Mathf.Floor(position.x), 0, Mathf.Floor(position.z));
    }
}