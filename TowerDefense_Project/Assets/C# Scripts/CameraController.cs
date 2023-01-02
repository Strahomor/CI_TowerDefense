using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cam_x_min = -486.9f;
    private float cam_x_max = 750.0f;
    private float cam_z_min = -295.0f;
    private float cam_z_max = 550.0f;

    private float cam_x_move_min = -410f;
    private float cam_x_move_max = 585f;
    private float cam_z_move_min = -150f;
    private float cam_z_move_max = 350f;

    public float hInput;
    public float vInput;

    public float sped = 150.0f;

    Vector3 direction1;
    Vector3 direction2;

    public GameObject Playa;

    //350, -150z
    //585, -410z
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        if (hInput < 0) { direction1 = (-(Vector3.right)); } else if (hInput > 0) { direction2 = (Vector3.right); }
        if (vInput < 0) { direction1 = (-(Vector3.up)); } else if (vInput > 0) { direction2 = (Vector3.up); }

        if (((Playa.transform.position.x < cam_x_move_min) && (Playa.transform.position.x > cam_x_min)) || (((Playa.transform.position.x > cam_x_move_max)) && (Playa.transform.position.x < cam_x_max))) { transform.Translate(Vector3.right * hInput * Time.deltaTime * sped); }
        if (((Playa.transform.position.z < cam_z_move_min) && (Playa.transform.position.z > cam_z_min)) || ((Playa.transform.position.z > cam_z_move_max) && (Playa.transform.position.z < cam_z_max))) { transform.Translate(Vector3.up * vInput * Time.deltaTime * sped); }
        

    }
}
