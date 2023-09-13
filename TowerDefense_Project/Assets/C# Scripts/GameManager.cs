using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int WaveCounter;
    public int EnemiesKilled;
    public int Score;
    public int HP;
    public int Euros;
    

    public int ChosenSpawner;
    public int EnemyToSpawn;
    public List<GameObject> SpawnPoints = new List<GameObject>();
    public List<GameObject> Enemies;
    public List<GameObject> Towers;
    //public List<GameObject> InstantiatedTowers;

    public double TotalSpawns;
    private double SpawnerSpawnLimit;
    public int EnemiesSpawned;
    public int EnemyLevel;

    private int ScienceLimit;
    private int TechLimit;
    private int EngineeringLimit;
    private int MathLimit;
    public List<GameObject> AvailableSpawners = new List<GameObject>();
    public List<string> SpawnerInventory = new List<string>();

    private List<int> TempInts;

    public float TimeInterval;
    public float PreRoundTimer;
    public double SpawnTimeDelay;
    private bool RoundOngoing;
    private bool SkipWait;
    private Vector3 ObjectDump = new Vector3(999.0f, 999.0f, 999.0f);

    public HealthBarLogic healthBar;

    public TMP_Text Cash;
    public TMP_Text ShopCash;
    public TMP_Text EnemiesLeft;
    public TMP_Text CenterMessage;
    public TMP_Text BottomRightMessage;

    public TMP_Text InvS;
    public TMP_Text InvT;
    public TMP_Text InvE;
    public TMP_Text InvM;

    public Canvas ShopUI;
    public Canvas InGameUI;

    public enum UIStates { Main, Shop};
    public UIStates currentstate;

    public GameObject MainUIPanel;
    public GameObject ShopUIPanel;
    public GameObject PauseUI;

    private string RoundMessage = "Next round in: ";

    // Start is called before the first frame update
    void Start()
    { 
        //currentstate = UIStates.Main;
        PauseUI.SetActive(false);
        ShopUIPanel.SetActive(false);
        MainUIPanel.SetActive(true);
        for (int i = 0; i <= 3; i++)
        {
            SpawnerInventory.Add("blank");
        }
        InvS.gameObject.SetActive(false);
        InvT.gameObject.SetActive(false);
        InvE.gameObject.SetActive(false);
        InvM.gameObject.SetActive(false);
        SkipWait = false;
        RoundOngoing = false;
        //PreRoundTime = 30;
        PreRoundTimer = 30;
        //InGameUI.gameObject.SetActive(true);
        //ShopUI.gameObject.SetActive(false);
        CenterMessage.gameObject.SetActive(true);
        BottomRightMessage.gameObject.SetActive(true);
        CenterMessage.text = RoundMessage + System.Convert.ToInt32(PreRoundTimer).ToString();
        BottomRightMessage.text = "Press B to open the shop!";
        Score = 0;
        Euros = 0;
        Cash.text = Euros.ToString();
        ShopCash.text = Euros.ToString();
        EnemiesLeft.text = TotalSpawns.ToString();
        HP = 100;
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

        WaveCounter = 0;
        TotalSpawns = 0;
        SpawnerSpawnLimit = TotalSpawns / 4;
        SpawnTimeDelay = System.Math.Exp(-EnemiesSpawned + 2);

        EnemyLevel = 1;
        healthBar = FindObjectOfType<HealthBarLogic>();
        healthBar.SetHealth(100);
        healthBar.SetMaxHealth(100);

        
    }

    private void FixedUpdate()
    {
        PreRoundTimer -= Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (WaveCounter == 0)
        {
            PreRound();
            BottomRightMessage.gameObject.SetActive(true);
        }

        if ((TimeInterval >= SpawnTimeDelay) && (EnemiesSpawned < TotalSpawns) && (RoundOngoing))
        {
            SpawnEnemy();
        }

        if ((EnemiesKilled >= TotalSpawns) && (WaveCounter != 0))
        {
            if (RoundOngoing)
            {
                RoundOngoing = !RoundOngoing;
                CenterMessage.gameObject.SetActive(true);
                BottomRightMessage.gameObject.SetActive(true);
            }
            PreRound();
            //WaveChange(); 
        }

        if ((Input.GetKeyDown(KeyCode.K) && (!RoundOngoing)))
        {
            SkipWait = true;
        }

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
        //GameObject towerClone = Instantiate(Towers[Chooser], SpawnPos, Towers[Chooser].transform.rotation);
        switch (Chooser)
        {
            case 0:
                if ((SpawnerInventory.Contains("blank")) || (SpawnerInventory.Contains("Tech")))
                {
                    GameObject towerClone0 = Instantiate(Towers[Chooser], SpawnPos, Towers[Chooser].transform.rotation);
                    towerClone0.tag = "TechTower";
                    towerClone0.AddComponent<TowerController>();
                    if (SpawnerInventory.Contains("blank") && (SpawnerInventory).Contains("Tech"))
                    {
                        SpawnerInventory.Remove("Tech");
                    }
                    else
                    {
                        SpawnerInventory.Remove("blank");
                    }
                    
                }
                else
                {
                    Debug.Log("No units available");
                }
                break;
            case 3:
                if ((SpawnerInventory.Contains("blank")) || (SpawnerInventory.Contains("Engineering")))
                {
                    GameObject towerClone1 = Instantiate(Towers[Chooser], SpawnPos, Towers[Chooser].transform.rotation);
                    towerClone1.tag = "EngineeringTower";
                    towerClone1.AddComponent<TowerController>();
                    if (SpawnerInventory.Contains("blank") && (SpawnerInventory).Contains("Engineering"))
                    {
                        SpawnerInventory.Remove("Engineering");
                    }
                    else
                    {
                        SpawnerInventory.Remove("blank");
                    }
                }
                else
                {
                    Debug.Log("No units available");
                }
                break;
            case 6:
                if ((SpawnerInventory.Contains("blank")) || (SpawnerInventory.Contains("Science"))) {
                    GameObject towerClone2 = Instantiate(Towers[Chooser], SpawnPos, Towers[Chooser].transform.rotation);
                    towerClone2.tag = "ScienceTower";
                    towerClone2.AddComponent<TowerController>();
                    if (SpawnerInventory.Contains("blank") && (SpawnerInventory).Contains("Science"))
                    {
                        SpawnerInventory.Remove("Science");
                    }
                    else
                    {
                        SpawnerInventory.Remove("blank");
                    }
                }
                else
                {
                    Debug.Log("No units available");
                }
                break;
            case 9:
                if ((SpawnerInventory.Contains("blank")) || (SpawnerInventory.Contains("Math"))){
                    GameObject towerClone3 = Instantiate(Towers[Chooser], SpawnPos, Towers[Chooser].transform.rotation);
                    towerClone3.tag = "MathTower";
                    towerClone3.AddComponent<TowerController>();
                    if (SpawnerInventory.Contains("blank") && (SpawnerInventory).Contains("Math"))
                    {
                        SpawnerInventory.Remove("Math");
                    }
                    else
                    {
                        SpawnerInventory.Remove("blank");
                    }
                }
                else
                {
                    Debug.Log("No units available");
                }
                break;
        }
    }

    void WaveChange()
    {
        
        if (WaveCounter != 0) { 
            TotalSpawns = System.Math.Pow(4, WaveCounter + 2); 
        }
        else { 
            TotalSpawns = 16;
        }
        WaveCounter += 1;
        SpawnerSpawnLimit = TotalSpawns / 4;
        //MathLimit = System.Convert.ToInt32(SpawnerSpawnLimit); TechLimit = System.Convert.ToInt32(SpawnerSpawnLimit); ScienceLimit = System.Convert.ToInt32(SpawnerSpawnLimit); EngineeingLimit = System.Convert.ToInt32(SpawnerSpawnLimit);
        ScienceLimit = 0; TechLimit = 0; EngineeringLimit = 0; MathLimit = 0;
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            AvailableSpawners.Add(SpawnPoints[i]);
        }
        EnemiesSpawned = 0;
        EnemiesKilled = 0;
        if (WaveCounter % 5 == 0)
        {
            EnemyLevel += 1;
        }
        RoundOngoing = true;
        //ShopUI.gameObject.SetActive(false);
        //InGameUI.gameObject.SetActive(true);
        ShopUIPanel.SetActive(false);
        MainUIPanel.SetActive(true);
        currentstate = UIStates.Main;
        EnemiesLeft.text = TotalSpawns.ToString();
        BottomRightMessage.text = "Press E to enter build mode!";
        Debug.Log("WaveChanged");
    }

    void PreRound()
    {
        if(PreRoundTimer <= 0) 
        {
            WaveChange();
            CenterMessage.gameObject.SetActive(false);
            PreRoundTimer = 30;
        }
        else if ((SkipWait == true) && (!RoundOngoing))
        {
            WaveChange();
            CenterMessage.gameObject.SetActive(false);
            PreRoundTimer = 30;
            SkipWait = false;
        }
        else
        {
            BottomRightMessage.text = "Press B to open the shop!";
            if (Input.GetKeyDown(KeyCode.B)) //&& (ShopOpened == false))
            {
                ActivatePanel();
                //Debug.LogError("Pressed once");
                //InGameUI.gameObject.SetActive(false);
                //ShopUI.gameObject.SetActive(true);
                //ShopOpened = true;
            }
            //else if ((Input.GetKeyDown(KeyCode.B)) && (ShopOpened == true))
            //{
               // ActivateCanvas();
                //InGameUI.gameObject.SetActive(true);
                //ShopUI.gameObject.SetActive(false);
                //ShopOpened = false;

            //}
            CenterMessage.text = RoundMessage + System.Convert.ToInt32(PreRoundTimer).ToString();
        }
    }

    void ZaWarudo()
    {
        
    }
    //void ActivateCanvas()
    //{
    //    if (ShopOpened == true)
    //    {
    //        ShopUI.gameObject.SetActive(false);
    //        InGameUI.gameObject.SetActive(true);
    //        for (int i = 0; i<InGameUI.transform.childCount; i++)
    //        {
    //            InGameUI.transform.GetChild(i).gameObject.SetActive(true);
    //        }
    //        ShopOpened = false;
    //    }
    //    else if (ShopOpened == false)
    //    {
    //        ShopUI.gameObject.SetActive(true);
    //        InGameUI.gameObject.SetActive(false);
    //        for (int i = 0; i < ShopUI.transform.childCount; i++)
    //        {
    //            ShopUI.transform.GetChild(i).gameObject.SetActive(true);
    //        }
    //        ShopOpened = true;
    //    }
    //}

    void ActivatePanel()
    {
        if (currentstate == UIStates.Shop)
        {
            currentstate = UIStates.Main;
            Debug.LogWarning("Switched to main");
            switch (currentstate)
            {
                case UIStates.Main:
                    ShopUIPanel.SetActive(false);
                    MainUIPanel.SetActive(true);
                    break;
                case UIStates.Shop:
                    MainUIPanel.SetActive(false);
                    ShopUIPanel.SetActive(true);
                    break;
            }
        }
        else if (currentstate == UIStates.Main)
        {
            currentstate = UIStates.Shop;
            Debug.LogWarning("Switched to shop");
            switch (currentstate)
            {
                case UIStates.Main:
                    ShopUIPanel.SetActive(false);
                    MainUIPanel.SetActive(true);
                    break;
                case UIStates.Shop:
                    MainUIPanel.SetActive(false);
                    ShopUIPanel.SetActive(true);
                    break;
            }
        }
    }
}
