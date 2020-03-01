using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static bool PausedGame = false;
    public GameObject InGameMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausedGame)
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
    }

    public void Resume() 
    {
        InGameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PausedGame = false;
    }

    void Pause() 
    {
        InGameMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PausedGame = true;
    }

    public void Retry() 
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }

    public void QuitGame() 
    {
        SceneManager.LoadScene("Main Menu");
    }
}
