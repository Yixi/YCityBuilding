using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class Ground : MonoBehaviour
{
    public GameObject environment;
    public List<Nature> treePrefbs;

    private GameManager _gameManager;
    private BuildingManager _buildingManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _buildingManager = GameObject.Find("Game Manager").GetComponent<BuildingManager>();

        InitGround();
        // InitCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            InitGround();
        }
    }

    void InitGround()
    {
        transform.position = new Vector3(_gameManager.mapWidth / 2, 0, _gameManager.mapHeight / 2);
        transform.localScale = new Vector3(_gameManager.mapWidth, 1, _gameManager.mapHeight);
    }

    public void InitTrees()
    {
        foreach (var i in Enumerable.Range(0, Random.Range(15, 25)))
        {
            var randomX = Random.Range(1, _gameManager.mapWidth - 1);
            var randomZ = Random.Range(1, _gameManager.mapHeight - 1);

            if (!_buildingManager._buildings[randomX, randomZ])
            {
                _buildingManager._buildings[randomX, randomZ] = Instantiate(
                    treePrefbs[Random.Range(0, treePrefbs.Count)],
                    new Vector3(randomX, 0, randomZ),
                    Quaternion.identity,
                    environment.transform
                );
            }
        }
    }
}