 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GameManager
{
    public float Speed = 10f;
    private Transform target;
    private Transform previous;
    private Transform roaming;
    private int endpointsindex;
    private int waypointIndex = 0;
    private int spawnerIndex = 0;

    private List<int> UsedIndexes = new List<int>();

    void Start()
    {
        target = Waypoints.points[0];
        roaming = EndPoints.endpoints[0];
        endpointsindex = 0;
        GetNextWaypoint();
        
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4){
            GetNextWaypoint();
        }


        if ((Vector3.Distance(transform.position, target.position) <= 0.4) && (waypointIndex >= Waypoints.points.Length - 1))
        {
            //Debug.Log("Roaming");
            RoamingWaypoints();
        }
    }

    void GetNextWaypoint()
    {
        
        if (waypointIndex <= Waypoints.points.Length - 1)
        {
            //Debug.Log("Targeted");
            target = Waypoints.points[waypointIndex];
            Vector3 dir = target.position - transform.position;
            waypointIndex++;
        }
    }

    void RoamingWaypoints()
    {
        //Debug.Log(UsedIndexes.Count);
        if (UsedIndexes.Count == EndPoints.endpoints.Length)
        {
            for (int j = 0; j <= UsedIndexes.Count; j++)
            {
                UsedIndexes.Remove(UsedIndexes[j]);
            }
            Debug.Log("Removed all");
        }

        Debug.Log(EndPoints.endpoints.Length);
        for (int i = 0; i <= (EndPoints.endpoints.Length)-1; i++)
        {
            if ((Vector3.Distance(transform.position, EndPoints.endpoints[i].position) <= 100.0) && (!(UsedIndexes.Contains(i))))
            {
                endpointsindex = i;
                //Debug.Log(endpointsindex);
                UsedIndexes.Add(i);
                break;
            }
        }
        target = EndPoints.endpoints[endpointsindex];
        Vector3 dir = target.position - transform.position;
        


        //endpointsindex = Random.Range(0, EndPoints.endpoints.Length);
    }
}
