using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameManager GameManager;
    public string buttonName;

    public void PauseButtonPress()
    {
        GameManager = FindObjectOfType<GameManager>();
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        switch (buttonName)
        {
            case "ResumeButton":
                Debug.Log("Resume");
                GameManager.ZaWarudo();
                break;
            case "RestartButton":
                Debug.Log("Restart");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                GameManager.ZaWarudo();
                break;
            case "QuitButton":
                //Debug.Log("Quit");
                SceneManager.LoadScene("MainMenu");
                break;
            case "ExitButton":
                Application.Quit();
                break;
            case "ScreenSizeChanger":
                Debug.Log("Changed screen size");
                Screen.fullScreen = !Screen.fullScreen;
                if (Screen.fullScreen)
                {
                    GameManager.FullScreenMode.text = "On";
                }
                else
                {
                    GameManager.FullScreenMode.text = "Off";
                }
                break;
            default:
                Debug.LogWarning("Something went wrong (PauseUI)");
                break;

        }
    }
}
