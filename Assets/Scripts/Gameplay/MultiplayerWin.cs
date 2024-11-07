using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultiplayerWin : MonoBehaviour
{
    private bool Player1Won = false;
    private bool Player2Won = false;

    public Timer playerUI;
    public GameObject Winner;
    [SerializeField] private TextMeshProUGUI WinnerText;

    private void Update()
    {
        if (Player1Won)
        //If Player 1 won the level
        {
            playerUI.PlayerUI.SetActive(false);
            Winner.SetActive(true);
            WinnerText.text = "Player 1 Wins!";
        }
        else if (Player2Won)
        // If Player 2 won the level.
        {
            playerUI.PlayerUI.SetActive(false);
            Winner.SetActive(true);
            WinnerText.text = "Player 2 Wins!";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Time.timeScale = 0f;
        if (collision.gameObject.CompareTag("Player 1"))
        {
            // If Player 1 touches flag first, they win.
            Player1Won = true;
        }
        else if (collision.gameObject.CompareTag("Player 2"))
        {
            // If Player 2 touches flag first, they win.
            Player2Won = true;
        }
    }
}
