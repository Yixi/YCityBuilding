using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Building[,] _buildings;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _buildings = new Building[_gameManager.mapWidth, _gameManager.mapHeight];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void addBuilding(Building building, Vector3 position)
    {
        _buildings[(int) position.x, (int) position.z] = Instantiate(building, position, Quaternion.identity);
    }

    public bool IsHaveBuilding(Vector3 position)
    {
        return _buildings[(int) position.x, (int) position.z];
    }

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        return new Vector3(Mathf.Floor(position.x), 0, Mathf.Floor(position.z));
    }
}