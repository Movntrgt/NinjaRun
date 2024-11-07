using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class PlayerDeath : MonoBehaviour
{
    public LevelData LD;

    // References to other scripts/components
    private Rigidbody2D rb;
    private Animator animator;

    private float respawnPointx;
    private float respawnPointy;

    public int Level1TotalDeaths;
    public int Level2TotalDeaths;

    // Variables needed for outputtin Player Death Counter
    public int levelDeathCounter;
    [SerializeField] private TextMeshProUGUI DeathCount;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Store the coordinates of the player's starting position as the respawn point
        respawnPointx = transform.position.x;
        respawnPointy = transform.position.y;



        string[] lines1 = File.ReadAllLines("level1_data.txt");
        string[] values1 = lines1[1].Split(',');
        Level1TotalDeaths = int.Parse(values1[1]);

        string[] lines2 = File.ReadAllLines("level2_data.txt");
        string[] values2 = lines2[1].Split(',');
        Level2TotalDeaths = int.Parse(values2[1]);


        // Reset Death counter at start of level.
        levelDeathCounter = 0; 
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with an object with the tag "Trap"
        if (collision.gameObject.CompareTag("Trap")) 
        {
            Death();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Death();
        }
        else if (collision.gameObject.CompareTag("Out of Bounds"))
        {
            Death();
        }
    }

    private void Death()
    {
        rb.bodyType = RigidbodyType2D.Static; // Prevents the user from moving when they die.
        animator.SetTrigger("Death"); // The Death animation is played.

        // Death counter only applies to 1-player mode.
        if (LD.isPlayer1Mode)
        {
            levelDeathCounter++; 
            // Death counter for current level is incremented by one each time the player dies.
            DeathCount.text = "Death Count: " + levelDeathCounter;

            if (LD.isLevel1) // If Level 1 is being played
            {
                Level1TotalDeaths++;
                // Level 1 total death counter is increased
            }
            else
            {
                Level2TotalDeaths++;
                // Level 2 total death counter is increased
            }
            
        }
    }

    private void Respawn()
    {
        // Player is respwaned at the starting point of the level.
        transform.position = new Vector2(respawnPointx, respawnPointy);
        animator.ResetTrigger("Death"); // Death animation trigger is reset
        rb.bodyType = RigidbodyType2D.Dynamic; // Allows player to move again
    }


}

