using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Class representing the time after the game is over.
/// </summary>
public class GameOver : MonoBehaviour
{
    /// <summary>
    /// reference to the textbox displaying the number of survived rounds
    /// </summary>
    public Text roundsText;

    /// <summary>
    /// Initializer method for when the object is enabled (game over).
    /// </summary>
    void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    /// <summary>
    /// Method handling the player choice of playing the game again. Reloads the scene.
    /// </summary>
    public void Retry()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Method handling the player choice of going to main menu. Loads the main menu scene.
    /// </summary>
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
