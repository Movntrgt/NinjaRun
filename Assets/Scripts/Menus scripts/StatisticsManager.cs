using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class StatisticsManager : MonoBehaviour
{
    // Stores the file names of the External text files
    private string Level1Data = "level1_data.txt";
    private string Level2Data = "level2_data.txt";

    // Variables that control Unity UI objects
    public GameObject StatisticsPanel;
    public GameObject Level1Panel;
    public GameObject Level2Panel;
    private bool Level1isOpen = false;
    private bool Level2isOpen = false;

    //Variables that display the Death counts and Best Times for Level 1 and 2
    public TextMeshProUGUI L1QuickestTimeText;
    public TextMeshProUGUI L1TotalDeaths;

    public TextMeshProUGUI L2QuickestTimeText;
    public TextMeshProUGUI L2TotalDeaths;

    private void Start()
    {
        // Ensure Main Panel is only one active
        StatisticsPanel.SetActive(true);
        Level1Panel.SetActive(false);
        Level2Panel.SetActive(false);

    }

    private void Update()
    {
        BackButton();
    }

    public void Level1()
    {
        // Main Statistics panel is deactivated
        // Level 1 Statistics panel is activated
        StatisticsPanel.SetActive(false);
        Level1Panel.SetActive(true);
        Level1isOpen = true;

        // Check if the Level 1 statistics file exists
        if (File.Exists(Level1Data))
        {
            using (StreamReader reader = new StreamReader(Level1Data))
            {
                // Read from Level 1 Statistics text file
                reader.ReadLine();

                string line = reader.ReadLine();

                // If a string has been read
                if (line != null)
                {
                    // Split the string into an array of substrings
                    string[] values = line.Split(',');

                    // Convert the strings into relevant values
                    float time = float.Parse(values[0]);
                    int deaths = int.Parse(values[1]);

                    // Display Values onto players UI
                    L1QuickestTimeText.text = "Best time to complete: " + time;
                    L1TotalDeaths.text = "Total Deaths: " + deaths;
                }
            }
        }
    }

    public void Level2()
    {
        // Main Statistics panel is deactivated
        // Level 2 Statistics panel is activated
        StatisticsPanel.SetActive(false);
        Level2Panel.SetActive(true);
        Level2isOpen = true;

        // Check if the Level 2 statistics file exists
        if (File.Exists(Level2Data))
        {
            using (StreamReader reader = new StreamReader(Level2Data))
            {
                // Read from Level 2 statistics text file
                reader.ReadLine();

                string line = reader.ReadLine();

                // If a string has been read
                if (line != null)
                {
                    // Split the string into an array of substrings
                    string[] values = line.Split(',');

                    // Convert strings into relevant values
                    float time = float.Parse(values[0]);
                    int deaths = int.Parse(values[1]);
                    
                    // Display Values onto Player's UI
                    L2QuickestTimeText.text = "Best time to complete: " + time;
                    L2TotalDeaths.text = "Total Deaths: " + deaths;
                }
            }
        }
    }

    public void BackButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        // If the escape key is pressed
        {
            if (Level1isOpen)
            // If the Level 1 statistics are being shown
            {
                // Statistics for Level 1 are closed, Main Statistics panel reopens
                Level1Panel.SetActive(false);
                StatisticsPanel.SetActive(true);
                Level1isOpen = false;
            }
            else if (Level2isOpen)
            // If the Level 2 statistics are being shown
            {
                // Statistics for Level 2 are closed, Main Statistics panel reopens
                Level2Panel.SetActive(false);
                StatisticsPanel.SetActive(true);
                Level2isOpen = false;
            }
            else
            {
                // Load Main Menu
                SceneManager.LoadScene(1);
            }
        }
    }
}
