using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LevelSelection()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // Changes from Main Menu scene to level Selection Scene
        // The Main Menu scene has the build index of 1, Level Selection scene has build index of 2
    }

    public void Statistics()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        // Changes from Main Menu scene to level Selection Scene
        // Statistics scene has build index of 3
    }

    public void ControlsGuide()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        // Changes from Main Menu scene to level Selection Scene
        // Controls Guide scene has build index of 4
    }

    public void QuitGame()
    {
        Application.Quit();
        // Game closes
    }
}
