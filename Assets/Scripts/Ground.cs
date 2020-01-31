using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Ground : MonoBehaviour
{
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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
}