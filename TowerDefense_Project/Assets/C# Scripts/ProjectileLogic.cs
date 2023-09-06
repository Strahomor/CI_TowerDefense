using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : TowerController
{
    private Vector3 Target;
    public float speed;
    bool ded;

    private GameObject prev;
    // Start is called before the first frame update
    void Start()
    {
        speed = 30;
        ded = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1, 0);
        //if ((this.transform.parent.GetComponent<TowerController>().Targets[0] != prev) && (first != true))
        //{
        //    Destroy(this);
        //}
        
        var step = speed * Time.deltaTime;
        
        if (this.transform.parent.GetComponent<TowerController>().Targets.Count == 0)
        {
            //Debug.Log("List is empty");
            Destroy(gameObject);
        }
        else if (this.transform.parent.GetComponent<TowerController>().Targets.Count != 0)
        {
            prev = this.transform.parent.GetComponent<TowerController>().Targets[0];
            Target = this.transform.parent.GetComponent<TowerController>().Targets[0].transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, Target, step);

        if (Vector3.Distance(transform.position, Target) < 0.001f)
        {
            ded = true;
            prev = this.transform.parent.GetComponent<TowerController>().Targets[0];
            Destroy(this.transform.parent.GetComponent<TowerController>().Targets[0]);
            //Debug.Log(Targets[0].name);
            
        }
        if (ded)
        {
            Destroy(gameObject);
            this.transform.parent.GetComponent<TowerController>().Targets.RemoveAt(0);
            ded = false;
            GameManager.transform.GetComponent<GameManager>().Score += 10;
        }

        

    }
}
