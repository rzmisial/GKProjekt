using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class representing the maing menu actions.
/// </summary>
public class MainMenu : MonoBehaviour {

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
