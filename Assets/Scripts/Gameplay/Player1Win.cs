using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player1Win : MonoBehaviour
{
    public bool Player1Won;
    // This variable is to be called by the Timer code.

    private void Start()
    {
        Player1Won = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Player 1 Won");
            Player1Won = true;

        }
    }
}
