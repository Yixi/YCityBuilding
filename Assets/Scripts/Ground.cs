using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Ground : MonoBehaviour
{
    [SerializeField] private int width = 1;
    [SerializeField] private int height = 1;
    // Start is called before the first frame update
    void Start()
    {
        InitGround();   
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
        transform.position = new Vector3(width / 2, 0,  height / 2);
        transform.localScale = new Vector3(width, 1, height);
    }
}
