using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Object[,] _buildings;
    public GameObject buildingParent;
    
    private GameManager _gameManager;
    private Ground _ground;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _ground = GameObject.Find("Ground").GetComponent<Ground>();
        _buildings = new Object[_gameManager.mapWidth, _gameManager.mapHeight];
        _ground.InitTrees();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void addBuilding(Building building, Vector3 position)
    {
        var build = _buildings[(int) position.x, (int) position.z];
        if (build && build.GetType() != typeof(Building))
        {
            Destroy(build);
        }
        _buildings[(int) position.x, (int) position.z] =
            Instantiate(building, position, Quaternion.identity, buildingParent.transform);
    }

    public bool IsHaveBuilding(Vector3 position)
    {
        return _buildings[(int) position.x, (int) position.z] && _buildings[(int) position.x, (int) position.z].GetType() == typeof(Building);
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        return new Vector3(Mathf.Floor(position.x), 0, Mathf.Floor(position.z));
    }
}