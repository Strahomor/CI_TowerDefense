using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopButtons : MonoBehaviour
{
    public string buttonName;
    public GameManager GameManager;
    public List<string> TowerTypes;
    //public HealthBarLogic healthBar;

    private void Awake()
    {
        TowerTypes.Add("blank");
        TowerTypes.Add("Science");
        TowerTypes.Add("Tech");
        TowerTypes.Add("Engineering");
        TowerTypes.Add("Math");
    }
    public void ShopButtonPress()
    {
        GameManager = FindObjectOfType<GameManager>();
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        //Debug.Log(buttonName);
        //healthBar=FindObjectOfType<HealthBarLogic>();
        switch (buttonName)
        {
            case "ScienceButton":
                if (GameManager.Euros >= 200)
                {
                    GameManager.SpawnerInventory.Add("Science");
                    GameManager.Euros -= 200;
                    GameManager.ShopCash.text = GameManager.Euros.ToString();
                    GameManager.Cash.text = GameManager.Euros.ToString();
                }
                else
                {
                    Debug.LogWarning("BROKE");
                }
                break;
            case "TechButton":
                if (GameManager.Euros >= 200)
                {
                    GameManager.SpawnerInventory.Add("Tech");
                    GameManager.Euros -= 200;
                    GameManager.ShopCash.text = GameManager.Euros.ToString();
                    GameManager.Cash.text = GameManager.Euros.ToString();
                }
                else
                {
                    Debug.LogWarning("BROKE");
                }
                break;
            case "EngineeringButton":
                if (GameManager.Euros >= 200)
                {
                    GameManager.SpawnerInventory.Add("Engineering");
                    GameManager.Euros -= 200;
                    GameManager.ShopCash.text = GameManager.Euros.ToString();
                    GameManager.Cash.text = GameManager.Euros.ToString();
                }
                else
                {
                    Debug.LogWarning("BROKE");
                }
                break;
            case "MathButton":
                if (GameManager.Euros >= 200)
                {
                    GameManager.SpawnerInventory.Add("Math");
                    GameManager.Euros -= 200;
                    GameManager.ShopCash.text = GameManager.Euros.ToString();
                    GameManager.Cash.text = GameManager.Euros.ToString();
                }
                else
                {
                    Debug.LogWarning("BROKE");
                }
                break;
            case "RandomButton":
                if (GameManager.Euros >= 100)
                {
                    var randint = Random.RandomRange(0, 6);
                    GameManager.SpawnerInventory.Add(TowerTypes[randint]);
                    GameManager.Euros -= 100;
                    GameManager.ShopCash.text = GameManager.Euros.ToString();
                    GameManager.Cash.text = GameManager.Euros.ToString();
                }
                else
                {
                    Debug.LogWarning("BROKE");
                }
                break;
            case "NukeButton":
                if (GameManager.Euros >= 5000)
                {
                    GameManager.Nukeable = true;
                    GameManager.Euros -= 5000;
                    GameManager.ShopCash.text = GameManager.Euros.ToString();
                    GameManager.Cash.text = GameManager.Euros.ToString();
                }
                break;
            case "HP Refill":
                if (GameManager.Euros >= 400)
                {
                    if (GameManager.HP < 100)
                    {
                        if (100 - GameManager.HP < 25)
                        {
                            GameManager.HP = 100;
                        }
                        else
                        {
                            GameManager.HP += 25;
                        }
                        GameManager.healthBar.SetHealth(GameManager.HP);
                    }
                    GameManager.Euros -= 400;
                    GameManager.ShopCash.text = GameManager.Euros.ToString();
                    GameManager.Cash.text = GameManager.Euros.ToString();
                }
                else
                {
                    Debug.LogWarning("BROKE");
                }
                break;
            default:
                Debug.Log("Nope.");
                break;
        }
    }
}
