using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int WaveCounter;
    public int EnemiesKilled;

    public int ChosenSpawner;
    public int EnemyToSpawn;
    public List<GameObject> SpawnPoints;
    public List<GameObject> Enemies;
    public List<GameObject> Towers;
    public List<GameObject> InstantiatedTowers;

    private double TotalSpawns;
    private double SpawnerSpawnLimit;

    private int ScienceLimit;
    private int TechLimit;
    private int EngineeingLimit;
    private int MathLimit;
    private List<GameObject> AvailableSpawners = new List<GameObject>();

    public float TimeInterval = 1;
    public float DelayTime = 2;
    private Vector3 ObjectDump = new Vector3(999.0f, 999.0f, 999.0f);

    // Start is called before the first frame update
    void Start()
    {
        Object[] prefabscollection = Resources.LoadAll("Prefabs/Towers/" );
        foreach (GameObject prefab in prefabscollection) { GameObject lo = (GameObject)prefab; Towers.Add(lo); }

        for (int i = 0; i < Towers.Count; i++)
        {
            GameObject ogTower = Instantiate(Towers[i], ObjectDump, Quaternion.identity);
            InstantiatedTowers.Add(ogTower);
            //Debug.Log("Instantiated tower");
        }

        WaveCounter = 1;
        TotalSpawns = 16;
        SpawnerSpawnLimit = TotalSpawns / 4;
    }

    // Update is called once per frame
    void Update()
    {
        TimeInterval += Time.deltaTime;
        if (EnemiesKilled >= TotalSpawns)
        {
            WaveChange(); 
        }
    }

    void SpawnEnemy()
    {
        ChosenSpawner = Random.Range(0, AvailableSpawners.Count);

        Vector3 SpawnPosition = new Vector3(SpawnPoints[ChosenSpawner].transform.position.x, SpawnPoints[ChosenSpawner].transform.position.y, SpawnPoints[ChosenSpawner].transform.position.z);
        GameObject enemyclone = Instantiate(Enemies[EnemyToSpawn], SpawnPosition , Enemies[EnemyToSpawn].transform.rotation);
        enemyclone.transform.parent = SpawnPoints[ChosenSpawner].transform;
    
    }
    public void SpawnTower(int Chooser, Vector3 SpawnPos)
    
    {
        GameObject towerClone = Instantiate(Towers[Chooser], SpawnPos, Towers[Chooser].transform.rotation);
        switch (Chooser)
        {
            case 0:
                towerClone.tag = "Science"; 
                break;
            case 3:
                towerClone.tag = "Tech";
                break;
            case 6:
                towerClone.tag = "Engineering";
                break;
            case 9:
                towerClone.tag = "Math";
                break;
        }
        towerClone.AddComponent<TowerController>();
    }

    void WaveChange()
    {
        WaveCounter += 1;
        TotalSpawns = System.Math.Pow(4, WaveCounter + 2);
        SpawnerSpawnLimit = TotalSpawns / 4;
        //ScienceLimit = round(SpawnerSpawnLimit; TechLimit = SpawnerSpawnLimit;
        for (int i = 0; i<SpawnPoints.Count; i++)
        {
            AvailableSpawners[i] = SpawnPoints[i];
        }
    }

    void ZaWarudo()
    {

    }
}
