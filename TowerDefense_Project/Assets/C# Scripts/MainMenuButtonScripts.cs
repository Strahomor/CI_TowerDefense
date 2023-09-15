using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuButtonScripts : MonoBehaviour
{
    public enum SMButtons {Start, Options}
    private SMButtons currentmenu;

    public GameObject StartMenu;
    public GameObject OptionsMenu;
    public GameObject HTPMenu;
    public GameObject TypingsMenu;

    public string buttonName;

    void Start()
    {
        currentmenu = SMButtons.Start;
        OptionsMenu.SetActive(false);
        HTPMenu.SetActive(false);
        TypingsMenu.SetActive(false);
        StartMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        switch (buttonName)
        {
            case "Start Button":
                SceneManager.LoadScene("SampleScene");
                break;
            case "Options Button":
                currentmenu = SMButtons.Options;
                StartMenu.SetActive(false);
                OptionsMenu.SetActive(true);
                break;
            case "Exit Button":
                Application.Quit();
                break;
            case "Back Button":
                currentmenu = SMButtons.Start;
                StartMenu.SetActive(true);
                OptionsMenu.SetActive(false);
                TypingsMenu.SetActive(false);
                HTPMenu.SetActive(false);
                break;
            case "ResButton2560":
                Screen.SetResolution(2560, 1440, true);
                break;
            case "ResButton1920":
                Screen.SetResolution(1920, 1080, true);
                break;
            case "ResButton1366":
                Screen.SetResolution(1366, 768, true);
                break;
            case "ResButton1280":
                Screen.SetResolution(1280, 720, true);
                break;
            case "Fullscreen Button":
                Debug.Log("Changed screen size");
                Screen.fullScreen = !Screen.fullScreen;
                break;
            case "HTP Button":
                StartMenu.SetActive(false);
                HTPMenu.SetActive(true);
                break;
            case "Typings Button":
                HTPMenu.SetActive(false);
                TypingsMenu.SetActive(true);
                break;
        }
    }
    
}
