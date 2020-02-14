using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float speed = 1.0f;

    public Stack<Tuple<float, float>> paths;

    private Vector3? _currentDestination = null;
    private Action _callback;

    // Start is called before the first frame update
    void Awake()
    {
        paths = new Stack<Tuple<float, float>>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (_currentDestination == null)
        {
            SetCurrentDestination();
        }

        if (_currentDestination != null)
        {
            transform.rotation = Quaternion.LookRotation(_currentDestination.Value - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, _currentDestination.Value, step);

            if (Vector3.Distance(transform.position, _currentDestination.Value) < 0.001f)
            {
                SetCurrentDestination();
            }
        }
    }

    public void AddPath(float x, float z)
    {
        paths.Push(new Tuple<float, float>(x, z));
    }
    
    public void AddEvent(Action callback)
    {
        _callback = callback;
    }

    private void SetCurrentDestination()
    {
        _currentDestination = null;
        if (paths.Count == 0) return;
        if (paths.Count == 1) _callback?.Invoke();
        var currentPath = paths.Pop();
        _currentDestination = new Vector3(currentPath.Item1, transform.position.y, currentPath.Item2);
    }
}
