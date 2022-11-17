using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int EnemyToSpawn;
    public List<GameObject> SpawnPoints;
    public List<GameObject> Enemies;
    public float TimeInterval = 1;
    public float DelayTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", DelayTime, TimeInterval);
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
}
