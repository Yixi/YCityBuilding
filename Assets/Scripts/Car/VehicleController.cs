using UnityEngine;

public class VehicleController : MonoBehaviour
{

    public Car carPrefb;
    
    public GameObject roadParent;

    // Start is called before the first frame update

    public void AddAutoDriveCar()
    {
        var roads = roadParent.GetComponentsInChildren<Road>();
        var placeRoad = roads[Random.Range(0, roads.Length)];
        var wayPoint = placeRoad.GetWayPoints()[0];

        var car = Instantiate(
            carPrefb,
            new Vector3(wayPoint.transform.position.x, 0, wayPoint.transform.position.z),
            Quaternion.identity,
            transform
        );


        void FindWayPoint()
        {
            Waypoint nexWayPoint;
            if (wayPoint.nextWaypoints.Count > 0)
            {
                nexWayPoint = wayPoint.nextWaypoints[Random.Range(0, wayPoint.nextWaypoints.Count)];
            }
            else
            {
                nexWayPoint = wayPoint;
            }

            car.AddPath(nexWayPoint.transform.position.x, nexWayPoint.transform.position.z);
            wayPoint = nexWayPoint;
        }

        car.AddEvent(FindWayPoint);
        FindWayPoint();
    }

}
