using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;
public class ProjectileLogic : MonoBehaviour
{
    private Vector3 Target;
    public float speed;
    private float dmg = 0.5f;
    private bool ded;
    private bool dmgd;
    private GameObject prev;
    public GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        speed = 30;
        ded = false;
        dmgd = false;
        dmg += this.transform.parent.GetComponent<TowerController>().lvl * 0.2f;
        GameManager = FindObjectOfType<GameManager>();
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
            this.transform.parent.GetComponent<TowerController>().Targets[0].GetComponent<EnemyController>().enemyhp -= dmg;
            Debug.Log(this.transform.parent.GetComponent<TowerController>().Targets[0].GetComponent<EnemyController>().enemyhp);
            if (this.transform.parent.GetComponent<TowerController>().Targets[0].GetComponent<EnemyController>().enemyhp <= 0)
            {
                this.transform.parent.GetComponent<TowerController>().xp += 0.5f;
                prev = this.transform.parent.GetComponent<TowerController>().Targets[0];
                Destroy(this.transform.parent.GetComponent<TowerController>().Targets[0]);
                //Debug.Log(Targets[0].name);
                ded = true;
            }
            else
            {
                dmgd = true;
            }
            
            
            
        }
        if (ded)
        {
            CashIn(this.transform.parent.GetComponent<TowerController>().Targets[0].tag);
            GameManager.Cash.text = GameManager.Euros.ToString();
            GameManager.ShopCash.text = GameManager.Euros.ToString();
            this.transform.parent.GetComponent<TowerController>().Targets.RemoveAt(0);
            ded = false;
            GameManager.transform.GetComponent<GameManager>().Score += 10;
            GameManager.EnemiesKilled += 1;
            GameManager.EnemiesLeft.text = (GameManager.TotalSpawns - GameManager.EnemiesKilled).ToString();
            Destroy(gameObject);
        }

        else if (dmgd)
        {
            dmgd = false;
            Destroy(gameObject);
        }

        

    }
    void CashIn(string enemytag)
    {
        if ((enemytag == "Science" && this.tag == "ProjectileS") || (enemytag == "Tech" && this.tag == "ProjectileT") || (enemytag == "Engineering" && this.tag == "ProjectileE") || (enemytag == "Math" && this.tag == "ProjectileM"))
        {
            GameManager.Euros += 20;
        }
        else
        {
            GameManager.Euros += 10;
            Debug.Log("Got money");
        }
    }
}
