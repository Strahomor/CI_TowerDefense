using UnityEngine;

//amongusaj neki counter
public class Waypoints : GameManager
{
    public static Transform[] points;
    public static Transform[] mathwaypoints1;
    public static Transform[] mathwaypoints2;
    public static Transform[] sciencewaypoints1;
    public static Transform[] sciencewaypoints2;
    public static Transform[] engineeringwaypoints1;
    public static Transform[] engineeringwaypoints2;
    public static Transform[] techwaypoints1;
    public static Transform[] techwaypoints2;

    private int mathcounter1 = 0;
    private int mathcounter2 = 0;
    private int sciencecounter1 = 0;
    private int sciencecounter2 = 0;
    private int engineeringcounter1 = 0;
    private int engineeringcounter2 = 0;
    private int techcounter1 = 0;
    private int techcounter2 = 0;

     void Start()
    {
        //Debug.LogWarning(GameObject.FindGameObjectsWithTag("EngineeringSpawner").Length);
        points = new Transform[transform.childCount];
        sciencewaypoints1 = new Transform[GameObject.FindGameObjectsWithTag("ScienceSpawner").Length-1];
        sciencewaypoints2 = new Transform[GameObject.FindGameObjectsWithTag("ScienceSpawnerAlt").Length];
        techwaypoints1 = new Transform[GameObject.FindGameObjectsWithTag("TechSpawner").Length-1];
        techwaypoints2 = new Transform[GameObject.FindGameObjectsWithTag("TechSpawnerAlt").Length];
        engineeringwaypoints1 = new Transform[GameObject.FindGameObjectsWithTag("EngineeringSpawner").Length-1];
        engineeringwaypoints2 = new Transform[GameObject.FindGameObjectsWithTag("EngineeringSpawnerAlt").Length];
        mathwaypoints1 = new Transform[GameObject.FindGameObjectsWithTag("MathSpawner").Length-1];
        mathwaypoints2 = new Transform[GameObject.FindGameObjectsWithTag("MathSpawnerAlt").Length];
        for (int i = 0; i < transform.childCount; i++)
        {
            //Debug.Log(i);
            //Debug.LogWarning(transform.childCount);
            //points[i] = transform.GetChild(i);
            if (transform.GetChild(i).tag == "MathSpawner") { mathwaypoints1[mathcounter1] = transform.GetChild(i); mathcounter1 += 1; }
            else if (transform.GetChild(i).tag == "MathSpawnerAlt") { mathwaypoints2[mathcounter2] = transform.GetChild(i); mathcounter2 += 1; }
            else if (transform.GetChild(i).tag == "TechSpawner") { techwaypoints1[techcounter1] = transform.GetChild(i); techcounter1 += 1; }
            else if (transform.GetChild(i).tag == "TechSpawnerAlt") { techwaypoints2[techcounter2] = transform.GetChild(i); techcounter2 += 1; }
            else if (transform.GetChild(i).tag == "ScienceSpawner") { sciencewaypoints1[sciencecounter1] = transform.GetChild(i); sciencecounter1 += 1; }
            else if (transform.GetChild(i).tag == "ScienceSpawnerAlt") { sciencewaypoints2[sciencecounter2] = transform.GetChild(i); sciencecounter2 += 1; }
            else if (transform.GetChild(i).tag == "EngineeringSpawner") { engineeringwaypoints1[engineeringcounter1] = transform.GetChild(i); engineeringcounter1 += 1; }
            else if (transform.GetChild(i).tag == "EngineeringSpawnerAlt") { engineeringwaypoints2[engineeringcounter2] = transform.GetChild(i); engineeringcounter2 += 1; }
            else { Debug.LogError("PISS"); }
        }
        for (int j = 0; j<sciencewaypoints1.Length; j++)
        {
            Debug.Log(sciencewaypoints1[j].name);
        }

    }

    
}
