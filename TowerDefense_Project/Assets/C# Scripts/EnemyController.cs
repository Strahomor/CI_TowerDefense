 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed = 10f;
    private Transform target;
    private Transform roaming;
    private int endpointsindex;
    private int waypointIndex = 0;
    //public static List<Transform> roam = EndPoints.endpoints;

    void Start()
    {
        target = Waypoints.points[0];
        roaming = EndPoints.endpoints[0];
        endpointsindex = 0;
        //roam = EndPoints.endpoints;
        GetNextWaypoint();
        
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4){
            GetNextWaypoint();
        }


        if ((Vector3.Distance(transform.position, target.position) <= 0.4) && (waypointIndex >= Waypoints.points.Length - 1))//if (waypointIndex > Waypoints.points.Length - 1)
        {
            Debug.Log("Roaming");
            RoamingWaypoints();
        }
    }

    void GetNextWaypoint()
    {
        
        if (waypointIndex <= Waypoints.points.Length - 1)
        {
            Debug.Log("Targeted");
            target = Waypoints.points[waypointIndex];
            Vector3 dir = target.position - transform.position;
            //transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
            waypointIndex++;
        }
    }

    void RoamingWaypoints()
    {
        //roaming = roam[Random.Range(0, (roaming.Length - 1))];
        target = EndPoints.endpoints[endpointsindex];
        Vector3 dir = target.position - transform.position;
        //transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
        endpointsindex = Random.Range(0, EndPoints.endpoints.Length);
    }
}
