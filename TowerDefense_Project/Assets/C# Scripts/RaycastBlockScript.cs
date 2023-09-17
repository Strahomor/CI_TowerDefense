using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBlockScript : MonoBehaviour
{
    public ProtoPlayerController Player;
    public Renderer cubeRenderer;
    private Color newCubeColor;
    // Start is called before the first frame update
    private void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
    }
    private void awake()
    {
        cubeRenderer = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.IsMoving)
        {
            Vector3 pos = new Vector3(Player.transform.position.x, Player.hit.point.y + 5, Player.transform.position.z);
            transform.position = pos;
        }
        
        if (Player.Buildable)
        {
            Color32 amongus = new Color32(49, 189, 86, 255);
            cubeRenderer.material.color = Color.green;
        }
        else if (!Player.Buildable)
        {
            Color32 amongus = new Color32(207, 14, 14, 255);
            cubeRenderer.material.color = Color.red;
        }
    }
}
