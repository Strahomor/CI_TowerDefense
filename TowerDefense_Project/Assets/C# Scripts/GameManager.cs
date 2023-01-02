using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int EnemyToSpawn;
    public List<GameObject> SpawnPoints;
    public List<GameObject> Enemies;
    public List<GameObject> Towers;
    public List<GameObject> InstantiatedTowers;

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
        GameObject towerClone = Instantiate(InstantiatedTowers[Chooser], SpawnPos, InstantiatedTowers[Chooser].transform.rotation);
    }
}
