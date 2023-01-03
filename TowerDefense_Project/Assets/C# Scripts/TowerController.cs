using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private int lvl;
    private float xp;
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

        // Update is called once per frame

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            LevelUp();
            //Debug.Log(lvl);
            Debug.Log(CurrentMeshName);
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
    }
    void UpgradeMesh()
    {
        if ((lvl % 5 == 0) && (lvl / 5 < 3))
        {

            //CurrentMeshName = currentMesh.name;
            //Debug.Log(CurrentMesh);
            //NextMeshNumber = CurrentMeshName.Substring(CurrentMeshName.Length - 1)[0]; //Debug.Log(NextMeshNumber);
            //NextMeshNumberInt = NextMeshNumber - '0' + 1; //Debug.Log(NextMeshNumberInt);
            //NextMeshNumber = System.Convert.ToChar(NextMeshNumberInt);
            //NextMeshNumberString = NextMeshNumber.ToString();
            //NextMeshName = CurrentMeshName.Remove(CurrentMeshName[-1]); NextMeshName.Insert(-1, NextMeshNumberString);

            currentMesh = CurrentMesh.GetComponent<MeshFilter>();
            currentRenderer = CurrentMesh.GetComponent<MeshRenderer>();
            NextMeshName = GameManager.Towers[StartingMeshIndex + lvl / 5].GetComponent<MeshFilter>().name;
            NextRendererName = GameManager.Towers[StartingMeshIndex + lvl / 5].GetComponent<MeshRenderer>().name; ;

            currentMesh.sharedMesh = Resources.Load<Mesh>(NextMeshName);
            currentRenderer = GameManager.Towers[StartingMeshIndex + lvl / 5].GetComponent<MeshRenderer>();
        }
    }
}
