using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class Timer : MonoBehaviour
{
    // Calling other C# scripts
    public Win win;
    public LevelData LD;

    private float currentTime;
    private float finishTime;
    public float bestTimeLevel1; // Stores the best time for level 1.
    public float bestTimeLevel2; // Stores the best time for level 2.

    // Links the game object to this script so that the UI can be altered.
    public GameObject LevelComplete;
    public GameObject PlayerUI;
    public GameObject NewHighScore;

    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private TextMeshProUGUI CompletionTimeText;
    [SerializeField] private TextMeshProUGUI NewHighScoreText;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;

        string[] lines1 = File.ReadAllLines("level1_data.txt");
        string[] values1 = lines1[1].Split(',');
        //bestTimeLevel1 = float.Parse(values1[0]);

        string[] lines2 = File.ReadAllLines("level2_data.txt");
        string[] values2 = lines2[1].Split(',');
        //bestTimeLevel2 = float.Parse(values2[0]);

        bestTimeLevel1 = 150;
        bestTimeLevel2 = 150;

        // Load Player UI when level starts
        PlayerUI.SetActive(true);
        // Allow time to flow again
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!win.gameWon) // If game has not been won
        {
            currentTime += Time.deltaTime; // Timer increases
            TimerText.text = "Time elapsed: " + currentTime.ToString("0.00"); // Timer is shown on player's UI
        }
        else if (win.gameWon && LD.isPlayer1Mode)
        // If the game has been won and is in Player 1 mode
        {
            // Stop player from moving after game has been won
            Time.timeScale = 0f;
            finishTime = currentTime;

            // Disable the Player's level UI
            PlayerUI.SetActive(false);

            if (LD.isLevel1 && finishTime < bestTimeLevel1)
            // If level 1 is being played and
            // their finishing time is quicker than the best time for the level 1.
            {
                // Activate new high score UI
                NewHighScore.SetActive(true);

                // New highscore is outputted onto the screen
                NewHighScoreText.text = "The new record for Level 1 is " + finishTime.ToString("0.00") + " seconds";

                // Replace old best time for level 1 with the new time
                bestTimeLevel1 = finishTime;
            }
            else if (!LD.isLevel1 && finishTime < bestTimeLevel2)
            // If level 2 is being played and
            // the player's finishing time is quicker than the best time for level 2.
            {
                // Activate new high score UI
                NewHighScore.SetActive(true);

                // New highscore is outputted onto the screen
                NewHighScoreText.text = "The new record for Level 2 is " + finishTime.ToString("0.00") + " seconds";

                // Replace old best time of level 2 with the new time
                bestTimeLevel2 = finishTime;

            }
            else // If the player's time was slower than the best time
            {
                // Activate Level Completion screen
                LevelComplete.SetActive(true);

                // Finising time is outputted onto the screen
                CompletionTimeText.text = "You completed the level in " + finishTime.ToString("0.00") + " seconds.";
                Debug.Log("Time taken: " + finishTime.ToString("0.00"));

            }
        }
    }
}
