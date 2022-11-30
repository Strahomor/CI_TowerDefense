using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        currentpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.Space) || (Input.GetKey(KeyCode.LeftControl) && ())))
        #region movement

        counter += 1;
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
            transform.position = new Vector3(transform.position.x, transform.position.y,z_min);
        }
        if (transform.position.z > z_max)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z_max);
        }


        if (!Collided) {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
        if (!Collided) {
            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
        }
        if (!Collided) {
            upInput = Input.GetAxis("Up");
            transform.Translate(Vector3.up * upInput * Time.deltaTime * speed);
        }

        if ((Collided))
        {
            resetpos = new Vector3(previouspos.x - currentpos.x, previouspos.y - currentpos.y, previouspos.z - currentpos.z);
            Debug.Log(resetpos.x); Debug.Log(resetpos.y); Debug.Log(resetpos.z);
            if (resetpos.x > 0)
            {
                changex = 4.5f;
            }
            else if (resetpos.x < 0)
            {
                changex = -4.5f;
            }
            else if (resetpos.y < 0)
            {
                changey = -4.5f;
            }
            else if (resetpos.y > 0)
            {
                changey = 4.5f;
            }
            if (resetpos.z > 0)
            {
                changez = 4.5f;
            }
            else if (resetpos.z < 0)
            {
                changez = -4.5f;
            }
            transform.position = new Vector3(currentpos.x + changex, currentpos.y + changey, currentpos.z + changez);
            //transform.Translate(resetpos);
            //transform.position = new Vector3(previouspos.x, previouspos.y, previouspos.z - 4.0f);
            Debug.Log("Position reset");
            changex = 0; changey = 0; changez = 0;
            Collided = false;
        }

        #endregion
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

    }
}
