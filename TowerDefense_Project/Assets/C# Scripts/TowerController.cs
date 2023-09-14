using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public int lvl;
    public float xp;
    private float xptreshhold = 5;
    private float treshholdvar = 0;
    private float basedmg;
    private float attacktime;
    private float tsinceattack;

    private string taggeroonskie;
    public GameManager GameManager;

    private int StartingMeshIndex;
    private GameObject CurrentMesh;
    private string CurrentMeshName;
    //private GameObject Target;
    GameObject setprojectile;



    private string NextMeshName;
    private string NextRendererName;
    private char NextMeshNumber;
    private string NextMeshNumberString;
    private int NextMeshNumberInt;

    //public GameObject emptyGameObject;

    MeshFilter currentMesh;
    MeshRenderer currentRenderer;
    SphereCollider sphcollider;

    public MeshFilter[] TowerMeshes;
    public List<GameObject> Targets;
    public List<GameObject> Projectiles;
    private List<string> TagsToTarget;
    // Start is called before the first frame update
    void Start()
    {
        lvl = 1;
        xp = 0f;
        tsinceattack = 0;
        attacktime = 1;
        taggeroonskie = gameObject.tag;
        switch (taggeroonskie)
        {
            case "ScienceTower":
                StartingMeshIndex = 0;
                setprojectile = Resources.Load("Prefabs/Projectiles/ProjectileScience") as GameObject;
                break;
            case "TechTower":
                StartingMeshIndex = 3;
                setprojectile = Resources.Load("Prefabs/Projectiles/ProjectileTech") as GameObject;
                break;
            case "EngineeringTower":
                StartingMeshIndex = 6;
                setprojectile = Resources.Load("Prefabs/Projectiles/ProjectileEngineering") as GameObject;
                break;
            case "MathTower":
                StartingMeshIndex = 9;
                setprojectile = Resources.Load("Prefabs/Projectiles/ProjectileMath") as GameObject;
                break;
        }
        Debug.Log(setprojectile.name);

        if (GameManager == null)
        {
            GameManager = FindObjectOfType<GameManager>();
        }

        CurrentMesh = gameObject;

        TowerMeshes = new MeshFilter[GameManager.Towers.Count];
        for (int i = 0; i < TowerMeshes.Length; i++)
        {
            TowerMeshes[i] = GameManager.Towers[i].GetComponent<MeshFilter>();
        }

        sphcollider = gameObject.GetComponent<SphereCollider>();
        Targets = new List<GameObject>();
        TagsToTarget = new List<string>() { "Science", "Math", "Engineering", "Technology" };
        Projectiles = new List<GameObject>();

        Object[] prefabscollection = Resources.LoadAll("Prefabs/Projectiles/");
        foreach (GameObject prefab in prefabscollection)
        {
            GameObject lo = (GameObject)prefab; Projectiles.Add(lo);

        }
    }
    void Update()
    {
        tsinceattack += Time.deltaTime/4;
        if (xp >= xptreshhold)
        {
            LevelUp();

        }
        if ((Targets.Count != 0) && (tsinceattack >= attacktime))   
        {
            Attack();
            if (Targets[0] == null)
            {
                Debug.Log("Dead, need new one");
            }
        }
    }
    void LevelUp()
    {
        switch (taggeroonskie)
        {
            case "ScienceTower":
                lvl += 1;
                UpgradeMesh();
                break;
            case "TechTower":
                lvl += 1;
                UpgradeMesh();
                break;
            case "EngineeringTower":
                lvl += 1;
                UpgradeMesh();
                break;
            case "MathTower":
                lvl += 1;
                UpgradeMesh();
                break;
        }
        treshholdvar += 1;
        xptreshhold += treshholdvar * 5;
        basedmg = lvl / 2;
    }
    void UpgradeMesh()
    {
        if ((lvl % 5 == 0) && (lvl / 5 < 3))
        {

            currentMesh = CurrentMesh.GetComponent<MeshFilter>();
            currentRenderer = CurrentMesh.GetComponent<MeshRenderer>();
            currentMesh.sharedMesh = TowerMeshes[StartingMeshIndex + 1].sharedMesh;
            currentRenderer = GameManager.Towers[StartingMeshIndex + lvl / 5].GetComponent<MeshRenderer>();
            StartingMeshIndex += 1;

            if (sphcollider.radius < 20)
            {
                sphcollider.radius = 20;
            }
            else
            {
                sphcollider.radius = 50;
            }


        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (TagsToTarget.Contains(other.tag))
        {
            Targets.Add(other.transform.gameObject);
            Debug.Log("Target acquired");
            Debug.Log(other.name);
        }

    }
    void Attack()
    {
        tsinceattack = 0;
        //Target = Targets[0];
        Vector3 ProjectileSpawn = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject projectile = Instantiate(setprojectile, ProjectileSpawn, transform.rotation, gameObject.transform);
        

    }
}

