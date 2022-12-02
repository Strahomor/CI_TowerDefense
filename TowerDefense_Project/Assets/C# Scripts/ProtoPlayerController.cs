using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ProtoPlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float upInput;
    Vector3 previouspos;
    Vector3 currentpos;
    Vector3 collidedpos;
    Vector3 resetpos;

    private float changex;
    private float changey;
    private float changez;

    private bool Collided = false;
    private bool Exited = false;
    private bool BuildMode = false;
    private int counter = 0;

    public float x_min = -486.90f;
    public float x_max = 850.0f;
    public float y_min = 30.0f;
    public float y_max = 275.0f;
    public float z_min = -295.0f;
    public float z_max = 630.0f;

    public float speed = 150.0f;
    public float flightspeed = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        #region Movement
        if (!Collided)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }

        else if (Collided)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(-(Vector3.right * horizontalInput * Time.deltaTime * speed/3));
            //Collided = false;
        }
        if (!Collided)
        {
            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
        }
        else if (Collided)
        {
            horizontalInput = Input.GetAxis("Vertical");
            transform.Translate(-(Vector3.forward * verticalInput * Time.deltaTime * speed/3));
            //Collided = false;
        }
        if (!Collided)
        {
            upInput = Input.GetAxis("Up");
            transform.Translate((Vector3.up * upInput * Time.deltaTime * speed));
        }
        else if (Collided)
        {
            horizontalInput = Input.GetAxis("Up");
            transform.Translate(-(Vector3.up * upInput * Time.deltaTime * speed/3));
            //Collided = false;
        }

        if (Collided && Exited == true)
        {
            counter += 1;
            if (counter == 17)
            {
                Collided = false;
                Exited = false;
                counter = 0;
            }
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.E))
        {
            BuildMode = !(BuildMode);
        }

        if (BuildMode)
        {
            Vector3 up = transform.TransformDirection(Vector3.up);
            RaycastHit hit;
            Ray ray = new Ray(transform.position, -(transform.up));
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.position.y <=20.0f)
                {
                    Debug.Log("Buildable area");
                }
                else
                {
                    Debug.Log("Area not usable");
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collidable"))
        {
            Collided = true;
            collidedpos = transform.position;
            Debug.Log("Collision detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Collidable"))
        {
            Exited = true;
        }
    }
}


