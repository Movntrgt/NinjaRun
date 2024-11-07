using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelectionManager : MonoBehaviour
{
    // Boolean variable that determines the player mode of a level
    private bool isPlayer1Mode = true;

    // Variables for text
    public TextMeshProUGUI SingleplayerText;
    public TextMeshProUGUI MultiplayerText;

    public Button ModeButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ModeButton.onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
            // Main Menu is loaded
        }
    }

    public void SwapPlayerMode()
    {
        if (isPlayer1Mode)
        {
            isPlayer1Mode = false;
            Debug.Log("Game is now in 2-Player mode");
            SingleplayerText.gameObject.SetActive(false);
            MultiplayerText.gameObject.SetActive(true);
            // The Text of the button changes depending on the Player Mode.
        }
        else
        {
            isPlayer1Mode = true;
            Debug.Log("Game is now in 1-Player mode");
            SingleplayerText.gameObject.SetActive(true);
            MultiplayerText.gameObject.SetActive(false);
        }
    }

    public void Level1()
    {
        if (isPlayer1Mode)
        { 
            SceneManager.LoadScene(5);
            // Changes from Level Selection to the 1-player version of  level 1
        }
        else
        {
            SceneManager.LoadScene(6);
            // Changes to 2-player version of level 1 
        }


    }

    public void Level2()
    {
        if (isPlayer1Mode)
        {
            SceneManager.LoadScene(7);
            // Changes from Level Selection to the 1-player version of level 2
        }
        else
        {
            SceneManager.LoadScene(8);
            // Changes to 2-player version level 2
        }
    }

}
