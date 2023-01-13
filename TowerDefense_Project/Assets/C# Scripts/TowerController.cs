using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private int lvl;
    private float xp;
    private float xptreshhold = 5;
    private float treshholdvar = 0;
    private float basedmg;

    private string taggeroonskie;
    public GameManager GameManager;

    private int StartingMeshIndex;
    private GameObject CurrentMesh;
    private string CurrentMeshName;

    

    private string NextMeshName;
    private string NextRendererName;
    private char NextMeshNumber;
    private string NextMeshNumberString;
    private int NextMeshNumberInt;

    MeshFilter currentMesh;
    MeshRenderer currentRenderer;

    public MeshFilter[] TowerMeshes;
    // Start is called before the first frame update
    void Start()
    {
        lvl = 1;
        xp = 0f;
        taggeroonskie = gameObject.tag;
        switch (taggeroonskie)
        {
            case "Science":
                StartingMeshIndex = 0;
                break;
            case "Tech":
                StartingMeshIndex = 3;
                break;
            case "Engineering":
                StartingMeshIndex = 6;
                break;
            case "math":
                StartingMeshIndex = 9;
                break;
        }

        if (GameManager == null)
        {
            GameManager = FindObjectOfType<GameManager>();
        }

        CurrentMesh = gameObject;

        TowerMeshes = new MeshFilter[GameManager.Towers.Count]; 
        for (int i = 0; i<TowerMeshes.Length; i++)
        {
            TowerMeshes[i] = GameManager.Towers[i].GetComponent<MeshFilter>();
        }
    }
    void Update()
    {
        if (xp >= xptreshhold)
        {
            LevelUp(); 

        }
    }

    void LevelUp()
    {
        switch (taggeroonskie)
        {
            case "Science":
                lvl += 1;
                UpgradeMesh();
                break;
            case "Tech":
                lvl += 1;
                UpgradeMesh();
                break;
            case "Engineering":
                lvl += 1;
                UpgradeMesh();
                break;
            case "Math":
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

        }
    }
}
