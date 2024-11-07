using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StatSaver : MonoBehaviour
{
    // References other C# scripts
    public LevelData LD;
    public Timer timer;
    public PlayerDeath PD;
    public Win win;

    // Specifies the location of the Level statistics files
    private string Level1Data = "level1_data.txt";
    private string Level2Data = "level2_data.txt";

    private void Start()
    {
        // Create text file for Level 1 data if it doesn't already exist
        if (!File.Exists(Level1Data))
        {
            using (StreamWriter writer = new StreamWriter(Level1Data))
            {
                writer.WriteLine("Time,Deaths");
            }
        }
        // Create text file for Level 2 data if it doesn't already exist
        if (!File.Exists(Level2Data))
        {
            using (StreamWriter writer = new StreamWriter(Level2Data))
            {
                writer.WriteLine("Time,Deaths");
            }
        }
    }

    // LateUpdate is called in every frame
    // but always after the Update method is called
    private void LateUpdate()
    {
        if (win.gameWon && LD.isLevel1)
        // If Level 1 is completed
        {
            // Overwrite the contents of the text file for Level 1 stats
            string data = timer.bestTimeLevel1.ToString("0.00") + "," + PD.Level1TotalDeaths;
            File.WriteAllText(Level1Data, "Time,Deaths\n" + data);
        }
        else if (win.gameWon && !LD.isLevel1)
        // If Level 2 is completed
        {
            // Overwrite the contents of the text file for Level 2 stats
            string data = timer.bestTimeLevel2.ToString("0.00") + "," + PD.Level2TotalDeaths;
            File.WriteAllText(Level2Data, "Time,Deaths\n" + data);
        }
    }
}
