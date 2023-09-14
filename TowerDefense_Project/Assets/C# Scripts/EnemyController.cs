 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float Speed = 10f;
    public int enemydmg = 10;
    public float enemyhp = 0.5f;

    private Transform target;
    private Transform previous;
    private Transform roaming;
    private int endpointsindex;
    private int waypointIndex = 0;
    private int spawnerIndex = 0;
    public string typetag;

    public Transform[] Motherland;
    private List<int> UsedIndexes = new List<int>();

    public GameManager GameManager;

    void Start()
    {
    }

    void Awake()
    {
        Speed = 20f;
        GameManager = FindObjectOfType<GameManager>();
        typetag = transform.parent.tag;
        switch (typetag)
        {
            default:
                break;
            case "ScienceSpawner":
                if (Random.Range(0, 2) == 0)
                {
                    Motherland = new Transform[Waypoints.sciencewaypoints1.Length];
                    for (int i = 0; i<Waypoints.sciencewaypoints1.Length; i++) { Motherland[i] = Waypoints.sciencewaypoints1[i]; }
                }
                else { Motherland = new Transform[Waypoints.sciencewaypoints2.Length]; for (int i = 0; i < Waypoints.sciencewaypoints2.Length; i++) { Motherland[i] = Waypoints.sciencewaypoints2[i]; } }

                break;
            case "TechSpawner":
                if (Random.Range(0, 2) == 0)
                {
                    Motherland = new Transform[Waypoints.techwaypoints1.Length];
                    for (int i = 0; i < Waypoints.techwaypoints1.Length; i++) { Motherland[i] = Waypoints.techwaypoints1[i]; }
                }
                else { Motherland = new Transform[Waypoints.techwaypoints2.Length]; for (int i = 0; i < Waypoints.techwaypoints2.Length; i++) { Motherland[i] = Waypoints.techwaypoints2[i]; } }

                break;
            case "EngineeringSpawner":
                if (Random.Range(0, 2) == 0)
                {
                    Motherland = new Transform[Waypoints.engineeringwaypoints1.Length];
                    for (int i = 0; i < Waypoints.engineeringwaypoints1.Length; i++) { Motherland[i] = Waypoints.engineeringwaypoints1[i]; }
                }
                else { Motherland = new Transform[Waypoints.engineeringwaypoints2.Length]; for (int i = 0; i < Waypoints.engineeringwaypoints2.Length; i++) { Motherland[i] = Waypoints.engineeringwaypoints2[i]; } }
                break;
            case "MathSpawner":
                if (Random.Range(0, 2) == 0) { Motherland = new Transform[Waypoints.mathwaypoints1.Length]; for (int i = 0; i < Waypoints.mathwaypoints1.Length; i++) { Motherland[i] = Waypoints.mathwaypoints1[i]; } }
                else { Motherland = new Transform[Waypoints.mathwaypoints2.Length]; for (int i = 0; i < Waypoints.mathwaypoints2.Length; i++) { Motherland[i] = Waypoints.mathwaypoints2[i]; } }
                break;

        }
        target = Motherland[0];
        waypointIndex = 0;
        endpointsindex = 0;
        GetNextWaypoint();
        //Debug.Log("k");

        enemydmg += (GameManager.EnemyLevel - 1) * 5;
        enemyhp += (GameManager.EnemyLevel - 1) * 0.5f;

        
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.5){
            GetNextWaypoint();
        }

    }

    void GetNextWaypoint()
    {
        
        if (waypointIndex <= Motherland.Length - 1)
        {
            //Debug.Log("Targeted");
            target = Motherland[waypointIndex];
            Vector3 dir = target.position - transform.position;
            waypointIndex++;
            //transform.Rotate(Motherland[waypointIndex].transform.rotation.x - transform.rotation.x, Motherland[waypointIndex].transform.rotation.y - transform.rotation.y, Motherland[waypointIndex].transform.rotation.z - transform.rotation.z);
            //Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720*Time.deltaTime);
            transform.forward = dir;
        }
        else if (waypointIndex > Motherland.Length-1) {
            GameManager.HP -= enemydmg; 
            GameManager.healthBar.SetHealth(GameManager.HP); 
            Debug.Log("HP lowered");
            GameManager.EnemiesKilled += 1;
            GameManager.EnemiesLeft.text = (GameManager.TotalSpawns - GameManager.EnemiesKilled).ToString();
            Destroy(gameObject);
            
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
