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

    private int Selected;
    private string Dick;

    public float x_min = -486.90f;
    public float x_max = 850.0f;
    public float y_min = 30.0f;
    public float y_max = 275.0f;
    public float z_min = -295.0f;
    public float z_max = 630.0f;

    public float speed = 150.0f;
    public float flightspeed = 200.0f;

    public float rotationz;
    public float rotationy;

    Rigidbody n_rigidbody;
    public GameManager GameManager;

    //List<GameObject> TowerSpawner = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        n_rigidbody = GetComponent<Rigidbody>();
        //for (int i = 0; i<=GameManager.Towers.Count-1; i++)
        //{
        //    TowerSpawner.Add(GameManager.Towers[i]);
        //}

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        rotationz = PlayerRotation.z;
        rotationy = PlayerRotation.y;

        #region Movement
        if (!Collided)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }

        else if (Collided)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(-(Vector3.right * horizontalInput * Time.deltaTime * speed/50));
            //if ((rotationz >= 0) && (rotationz<= 180))
            //{
            //   n_rigidbody.AddForce(transform.right * 5000, ForceMode.Impulse);
            //}
            //else { n_rigidbody.AddForce(-(transform.right * 5000), ForceMode.Impulse); }  
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
            transform.Translate(-(Vector3.forward * verticalInput * Time.deltaTime * speed/50));
            //if ((rotationz >= 90) && (rotationz <= 270))
            //{
            //    n_rigidbody.AddForce(transform.right * 5000, ForceMode.Impulse);
            //}
            //else { n_rigidbody.AddForce(-(transform.right * 5000), ForceMode.Impulse); }
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
            transform.Translate(-(Vector3.up * upInput * Time.deltaTime * speed/10));
            //Collided = false;
        }

        if (Collided && Exited)
        {
            counter += 1;
            if (counter == 20)
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
                if (hit.point.y <=20.0f)
                {
                    //Debug.Log("Buildable area");
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Selected = 0;
                        GameManager.SpawnTower(Selected, hit.point);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Selected = 3;
                        GameManager.SpawnTower(Selected, hit.point);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        Selected = 6;
                        GameManager.SpawnTower(Selected, hit.point);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        Selected = 9;
                        GameManager.SpawnTower(Selected, hit.point);
                    }
                    //Debug.Log(Selected);
                }
                else
                {
                    //Debug.Log("Area not usable");
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
            //Debug.Log("Collision detected");
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


