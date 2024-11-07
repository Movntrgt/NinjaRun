using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject PauseMenuUI;
    public GameObject PlayerUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        // If Esc or P keys are pressed
        {
            if (isPaused) // If game is already paused
            {
                Resume();
            }
            else // If game is not already paused
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        PlayerUI.SetActive(true);
        // Pause Menu is removed
        Time.timeScale = 1;
        // In game time resumes
        isPaused = false;
    }

    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        PlayerUI.SetActive(false);
        // Pause Menu is shown to player
        Time.timeScale = 0;
        // In game time stops
        isPaused = true;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(1);
        // Main Menu scene is loaded
    }
}
