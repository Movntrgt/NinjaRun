using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupManager : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene(1);
        // Chnages to Main Menu Scene
    }
}
