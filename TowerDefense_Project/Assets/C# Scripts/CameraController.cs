using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cam_x_min = -386.9f;
    private float cam_x_max = 800.0f;
    private float cam_z_min = -290.0f;
    private float cam_z_max = 450.0f;

    private float cam_x_move_min = -310f;
    private float cam_x_move_max = 485f;
    private float cam_z_move_min = -50f;
    private float cam_z_move_max = 250f;

    private float cam_rotate_x_min;
    private float cam_rotate_x_max;
    private float cam_rotate_y_min;
    private float cam_rotate_y_max;

    public float sensitivity = 5.0f;

    public float hInput;
    public float vInput;

    public float VerticalMouse;
    public float HorizontalMouse;

    private float prevRotation;
    public float ogX;
    public float ogY;
    public float ogZ;
    private Vector3 startingRotation;
    public Vector3 startingPosition;

    public float sped = 150.0f;

    private Vector3 direction1;
    private Vector3 direction2;

    public GameObject Playa;
    public GameObject Pivot;

    public bool lockedy;
    public bool lockedx;

    //350, -150z
    //585, -410z
    // Start is called before the first frame update
    void Start()
    {
        

    }
    private void Awake()
    {
        ogX = transform.rotation.eulerAngles.x;
        ogY = transform.rotation.eulerAngles.y;
        ogZ = transform.rotation.eulerAngles.z;
        startingPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.rotation = Quaternion.Euler(ogX, ogY, ogZ);
            transform.position = startingPosition;
        }
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        if (hInput < 0) { direction1 = (-(Vector3.right)); } else if (hInput > 0) { direction2 = (Vector3.right); }
        if (vInput < 0) { direction1 = (-(Vector3.up)); } else if (vInput > 0) { direction2 = (Vector3.up); }

        //transform.position = new Vector3(Playa.transform.position.x, transform.position.y, Playa.transform.position.z);

        if (((Playa.transform.position.x < cam_x_move_min) && (Playa.transform.position.x > cam_x_min)) || (((Playa.transform.position.x > cam_x_move_max)) && (Playa.transform.position.x < cam_x_max))) { transform.Translate(Vector3.right * hInput * Time.deltaTime * sped); }
        if (((Playa.transform.position.z < cam_z_move_min) && (Playa.transform.position.z > cam_z_min)) || ((Playa.transform.position.z > cam_z_move_max) && (Playa.transform.position.z < cam_z_max))) { transform.Translate(Vector3.up * vInput * Time.deltaTime * sped); }

        if (Input.GetMouseButton(1)) {
            //Debug.Log(startingRotation);
            prevRotation = transform.rotation.z;
            VerticalMouse = Input.GetAxis("Mouse Y");
            HorizontalMouse = Input.GetAxis("Mouse X");

            //if ((Input.GetAxis("Mouse X") != 0) && (lockedy == false)) { }

            if (Input.GetAxis("Mouse X") != 0) { transform.RotateAround(Pivot.transform.position, Pivot.transform.up, HorizontalMouse * sensitivity);  }
            if (Input.GetAxis("Mouse Y")!=0) { transform.RotateAround(Pivot.transform.position, Pivot.transform.right, -VerticalMouse * sensitivity);  }
        }
        if (transform.rotation.z != startingRotation.z) {
            transform.LookAt(Pivot.transform);
            }

    }
}
