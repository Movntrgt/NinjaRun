using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData : MonoBehaviour
{
    public bool isPlayer1Mode;
    public bool isLevel1;

    // Start is called before the first frame update
    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        int BuildIndex = scene.buildIndex;

        // Check if the level is in 1-Player mode
        if (BuildIndex == 5 || BuildIndex == 7)
        {
            isPlayer1Mode = true;
            Debug.Log("Level is in 1-Player mode");
            // Check if the Level is Level 1
            if (BuildIndex == 5)
            {
                isLevel1 = true;
                Debug.Log("Level is level 1");
            }
            else
            {
                isLevel1 = false;
            }

        }
        else // Level is in 2-Player mode
        {
            isPlayer1Mode = false;
            Debug.Log("In 2-player mode");
        }
    }
}
