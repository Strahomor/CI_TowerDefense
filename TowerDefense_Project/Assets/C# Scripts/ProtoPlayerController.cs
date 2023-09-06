using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ProtoPlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float upInput;

    private float lastaxish;
    private float lastaxisv;

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
    public float x_max = 820.0f;
    public float y_min = 30.0f;
    public float y_max = 275.0f;
    public float z_min = -330.0f;
    public float z_max = 550.0f;

    public float speed = 150.0f;
    public float flightspeed = 200.0f;

    public float rotationz;
    public float rotationy;

    private string KeyPressed;

    Rigidbody n_rigidbody;
    public GameManager GameManager;
    Animator animator;

    //List<GameObject> TowerSpawner = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        n_rigidbody = GetComponent<Rigidbody>();
        //for (int i = 0; i<=GameManager.Towers.Count-1; i++)
        //{
        //    TowerSpawner.Add(GameManager.Towers[i]);
        //}
        if (GameManager == null)
        {
            GameManager = FindObjectOfType<GameManager>();
        }
        animator = GetComponent<Animator>();

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
            horizontalInput = -(Input.GetAxis("Horizontal"));
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
            lastaxish = horizontalInput;
        }

        else if (Collided)
        {
            horizontalInput = -(Input.GetAxis("Horizontal"));
            transform.Translate(-(Vector3.right * lastaxish * Time.deltaTime * speed/50));
            //if ((rotationz >= 0) && (rotationz<= 180))
            //{
            //   n_rigidbody.AddForce(transform.right * 5000, ForceMode.Impulse);
            //}
            //else { n_rigidbody.AddForce(-(transform.right * 5000), ForceMode.Impulse); }  
            //Collided = false;
        }
        if (!Collided)
        {
            verticalInput = -(Input.GetAxis("Vertical"));
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
            lastaxisv = verticalInput;
        }
        else if (Collided)
        {
            horizontalInput = -(Input.GetAxis("Vertical"));
            transform.Translate(-(Vector3.forward * lastaxisv * Time.deltaTime * speed/50));
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

        
        if ((counter % 2 == 0) && (!(Collided)))
        {
            previouspos = transform.position;
        }
        currentpos = transform.position;

        if (transform.position.x < x_min)
        {
            transform.position = new Vector3(x_min, transform.position.y, transform.position.z);
        }
        if (transform.position.x > x_max)
        {
            transform.position = new Vector3(x_max, transform.position.y, transform.position.z);
        }

        if (transform.position.y < y_min)
        {
            transform.position = new Vector3(transform.position.x, y_min, transform.position.z);
        }
        if (transform.position.y > y_max)
        {
            transform.position = new Vector3(transform.position.x, y_max, transform.position.z);
        }

        if (transform.position.z < z_min)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z_min);
        }
        if (transform.position.z > z_max)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z_max);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.E))
        {
            BuildMode = !(BuildMode);
        }

        if (Input.GetKey(KeyCode.A)) { animator.SetBool("isTurningL", true); }
        else if (Input.GetKey(KeyCode.D)) { animator.SetBool("isTurningR", true); }
        else if (Input.GetKey(KeyCode.Space)) { animator.SetBool("isRising", true); }

        if (Input.GetKeyUp(KeyCode.A)) { animator.SetBool("isTurningL", false); }
        else if (Input.GetKeyUp(KeyCode.D)) { animator.SetBool("isTurningR", false); }
        else if (Input.GetKeyUp(KeyCode.Space)) { animator.SetBool("isRising", false); }


        switch (KeyPressed)
        {
            default:
                break;
            case "A":
                animator.SetBool("isTurningL", true);
                break;
            case "D":
                animator.SetBool("isTurningR", true);
                break;
            case "Space":
                animator.SetBool("isRising", true);
                break;
        }


        if (BuildMode)
        {
            Vector3 up = transform.TransformDirection(Vector3.up);
            RaycastHit hit;
            Ray ray = new Ray(transform.position, -(transform.up));
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 TowerPos = new Vector3(hit.point.x + 20.0f, hit.point.y, hit.point.z);
                if (hit.point.y <=20.0f)
                {
                    //Debug.Log("Buildable area");
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        Selected = 0;
                        GameManager.SpawnTower(Selected, TowerPos);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        Selected = 3;
                        GameManager.SpawnTower(Selected, TowerPos);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        Selected = 6;
                        GameManager.SpawnTower(Selected, TowerPos);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        Selected = 9;
                        GameManager.SpawnTower(Selected, TowerPos);
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


