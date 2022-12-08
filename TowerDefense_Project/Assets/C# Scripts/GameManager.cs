using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int EnemyToSpawn;
    public List<GameObject> SpawnPoints;
    public List<GameObject> Enemies;
    public List<GameObject> Towers;

    public float TimeInterval = 1;
    public float DelayTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnEnemy", DelayTime, TimeInterval);
        Object[] prefabscollection = Resources.LoadAll("Prefabs/Towers/" );
        foreach (GameObject prefab in prefabscollection) { GameObject lo = (GameObject)prefab; Towers.Add(lo); }
    }

    // Update is called once per frame
    void Update()
    {
        TimeInterval += Time.deltaTime;
    }

    void SpawnEnemy()
    {
        EnemyToSpawn = Random.Range(0, SpawnPoints.Count);
        Vector3 SpawnPosition = new Vector3(SpawnPoints[EnemyToSpawn].transform.position.x, SpawnPoints[EnemyToSpawn].transform.position.y, SpawnPoints[EnemyToSpawn].transform.position.z);
        GameObject enemyclone = Instantiate(Enemies[EnemyToSpawn], SpawnPosition , Enemies[EnemyToSpawn].transform.rotation);
    }
    public void SpawnTower(int Chooser, Vector3 SpawnPos)
    
    {
        GameObject towerClone = Instantiate(Towers[Chooser], SpawnPos, Towers[Chooser].transform.rotation);
    }
}
