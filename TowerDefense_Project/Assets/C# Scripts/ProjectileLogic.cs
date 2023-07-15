using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : TowerController
{
    private Vector3 Target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Target = transform.parent.position + this.transform.parent.GetComponent<TowerController>().Targets[0].transform.position;
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotato = new Vector3(0, 2, 0);
        transform.Rotate(rotato);
        transform.Translate(Target * Time.deltaTime * speed);
    }
}
