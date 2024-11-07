using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player2Win : MonoBehaviour
{
    public bool Player2Won;
    // This variable is to be called by the Timer code.
    private void Start()
    {
        Player2Won = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Player 2 Won");
            Player2Won = true;

        }
    }
}
