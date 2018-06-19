using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing the game manager - the master object contrilling the game flow.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// information on whether or not the game has already ended
    /// </summary>
    public static bool GameEnded = false;

    /// <summary>
    /// reference to the user interface
    /// </summary>
    public GameObject GameOverUI;

    /// <summary>
    /// Initializer method.
    /// </summary>
    void Start()
    {
        GameEnded = false;
    }

    /// <summary>
    /// Method checking if the player is still alive. If they're not - ends the game.
    /// </summary>
    void Update () {
    
 
        if (GameEnded)
	    {
            return;
	    }


	    if (PlayerStats.Lives <= 0)
	    {
	        EndGame();
	    }
	}

    /// <summary>
    /// Handles ending the game regardless of the reason. Sets the gameover UI as active.
    /// </summary>
    public void EndGame()
    {
        GameEnded = true;

        GameOverUI.SetActive(true);
    }
}
