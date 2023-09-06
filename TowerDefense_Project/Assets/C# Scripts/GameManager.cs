using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int WaveCounter;
    public int EnemiesKilled;
    public int Score;

    public int ChosenSpawner;
    public int EnemyToSpawn;
    public List<GameObject> SpawnPoints = new List<GameObject>();
    public List<GameObject> Enemies;
    public List<GameObject> Towers;
    //public List<GameObject> InstantiatedTowers;

    private double TotalSpawns;
    private double SpawnerSpawnLimit;
    public int EnemiesSpawned;

    private int ScienceLimit;
    private int TechLimit;
    private int EngineeringLimit;
    private int MathLimit;
    public List<GameObject> AvailableSpawners = new List<GameObject>();
    public List<string> SpawnerInventory = new List<string>();

    private List<int> TempInts;

    public float TimeInterval;
    public double SpawnTimeDelay;
    private Vector3 ObjectDump = new Vector3(999.0f, 999.0f, 999.0f);

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        Object[] prefabscollection = Resources.LoadAll("Prefabs/Towers/" );
        foreach (GameObject prefab in prefabscollection) { GameObject lo = (GameObject)prefab; Towers.Add(lo); }

        //for (int i = 0; i < Towers.Count; i++)
        //{
        //    GameObject ogTower = Instantiate(Towers[i], ObjectDump, Quaternion.identity);
        //    InstantiatedTowers.Add(ogTower);
            //Debug.Log("Instantiated tower");
        //}

        for (int i = 0; i<SpawnPoints.Count; i++)
        {
            AvailableSpawners.Add(SpawnPoints[i]);
        }
        //Debug.Log(SpawnPoints.Count);
        //Debug.Log(AvailableSpawners.Count);

        WaveCounter = 1;
        TotalSpawns = 16;
        SpawnerSpawnLimit = TotalSpawns / 4;
        SpawnTimeDelay = System.Math.Exp(-EnemiesSpawned + 2);

        for (int i = 0; i <= 3; i++)
        {
            SpawnerInventory.Add("blank");
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimeInterval += Time.deltaTime;
        if ((MathLimit >= System.Convert.ToInt32(SpawnerSpawnLimit)) || (ScienceLimit >= System.Convert.ToInt32(SpawnerSpawnLimit)) || (TechLimit >= System.Convert.ToInt32(SpawnerSpawnLimit)) || (EngineeringLimit >= System.Convert.ToInt32(SpawnerSpawnLimit)))
        {
            if (ScienceLimit >= System.Convert.ToInt32(SpawnerSpawnLimit))
            {
                AvailableSpawners.Remove(GameObject.FindGameObjectWithTag("ScienceSpawner"));
            }
            if (TechLimit >= System.Convert.ToInt32(SpawnerSpawnLimit))
            {
                AvailableSpawners.Remove(GameObject.FindGameObjectWithTag("TechSpawner"));
            }
            if (EngineeringLimit >= System.Convert.ToInt32(SpawnerSpawnLimit))
            {
                AvailableSpawners.Remove(GameObject.FindGameObjectWithTag("EngineeringSpawner"));
            }
            if (MathLimit >= System.Convert.ToInt32(SpawnerSpawnLimit))
            {
                AvailableSpawners.Remove(GameObject.FindGameObjectWithTag("MathSpawner"));
            }
        }

        if ((TimeInterval  >= SpawnTimeDelay) && (EnemiesSpawned < TotalSpawns))
        {
            SpawnEnemy();
        } 
        if (EnemiesKilled >= TotalSpawns)
        {
            WaveChange(); 
        }
    }

    void SpawnEnemy()
    {
        EnemiesSpawned += 1;
        ChosenSpawner = Random.Range(0, AvailableSpawners.Count);
        //Debug.Log(ChosenSpawner);
        switch (AvailableSpawners[ChosenSpawner].tag)
        {
            default:
                break;
            case "ScienceSpawner":
                ScienceLimit += 1;
                if (Random.Range(0, 101) > 10)
                {
                    EnemyToSpawn = 0;
                }
                else
                {
                    EnemyToSpawn = Random.Range(1, 4);
                }
                
                break;
            case "TechSpawner":
                TechLimit += 1;
                if (Random.Range(0, 101) > 10)
                {
                    EnemyToSpawn = 1;
                }
                else
                {
                    TempInts = new List<int>();
                    TempInts.Add(0); TempInts.Add(2); TempInts.Add(3);
                    EnemyToSpawn = TempInts[Random.Range(0, 3)];
                }
                break;
            case "EngineeringSpawner":
                EngineeringLimit += 1;
                if (Random.Range(0, 101) > 10)
                {
                    EnemyToSpawn = 2;
                }
                else
                {
                    TempInts = new List<int>();
                    TempInts.Add(0); TempInts.Add(1); TempInts.Add(3);
                    EnemyToSpawn = TempInts[Random.Range(0, 3)];
                }
                break;
            case "MathSpawner":
                MathLimit += 1;;
                if (Random.Range(0, 101) > 10)
                {
                    EnemyToSpawn = 3;
                }
                else
                {
                    TempInts = new List<int>();
                    TempInts.Add(0); TempInts.Add(1); TempInts.Add(2);
                    EnemyToSpawn = TempInts[Random.Range(0, 3)];
                }
                break;
        }

        Vector3 SpawnPosition = new Vector3(AvailableSpawners[ChosenSpawner].transform.position.x, AvailableSpawners[ChosenSpawner].transform.position.y, AvailableSpawners[ChosenSpawner].transform.position.z);
        //Debug.Log(AvailableSpawners[ChosenSpawner].name);
        GameObject enemyclone = Instantiate(Enemies[EnemyToSpawn], SpawnPosition, Enemies[EnemyToSpawn].transform.rotation, AvailableSpawners[ChosenSpawner].transform);
        enemyclone.tag = "Math";
        if (AvailableSpawners[ChosenSpawner] == null)
        {
            Debug.LogWarning("No spawner?");
        }
        //enemyclone.transform.parent = AvailableSpawners[ChosenSpawner].transform;

        if (EnemiesSpawned%SpawnerSpawnLimit-WaveCounter == 0)
        {
            SpawnTimeDelay = System.Math.Exp(-EnemiesSpawned + 2) + 1.5;
        }
        TimeInterval = 0;
    }
    public void SpawnTower(int Chooser, Vector3 SpawnPos)
    
    {
        GameObject towerClone = Instantiate(Towers[Chooser], SpawnPos, Towers[Chooser].transform.rotation);
        switch (Chooser)
        {
            case 0:
                towerClone.tag = "TechTower"; 
                break;
            case 3:
                towerClone.tag = "EngineeringTower";
                break;
            case 6:
                towerClone.tag = "ScienceTower";
                break;
            case 9:
                towerClone.tag = "MathTower";
                break;
        }
        towerClone.AddComponent<TowerController>();
    }

    void WaveChange()
    {
        WaveCounter += 1;
        TotalSpawns = System.Math.Pow(4, WaveCounter + 2);
        SpawnerSpawnLimit = TotalSpawns / 4;
        //MathLimit = System.Convert.ToInt32(SpawnerSpawnLimit); TechLimit = System.Convert.ToInt32(SpawnerSpawnLimit); ScienceLimit = System.Convert.ToInt32(SpawnerSpawnLimit); EngineeingLimit = System.Convert.ToInt32(SpawnerSpawnLimit);
        ScienceLimit = 0; TechLimit = 0; EngineeringLimit = 0; MathLimit = 0;
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            AvailableSpawners.Add(SpawnPoints[i]);
        }
        EnemiesSpawned = 0;
        Debug.Log("WaveChanged");
    }

    void ZaWarudo()
    {
        
    }
}
