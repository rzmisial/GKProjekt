using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class representing the pause menu.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// reference to the pause user interface
    /// </summary>
    public GameObject PauseUI;

    /// <summary>
    /// Method responsible for toggling the pause menu if the player presses the keys escape or 'P'.
    /// </summary>
	void Update () {
	    if (Input.GetKeyDown((KeyCode.Escape)) || Input.GetKeyDown(KeyCode.P))
	    {
	        TogglePause();
	    }
	}

    /// <summary>
    /// Method performing the pause menu toggle (stops time on pause).
    /// </summary>
    public void TogglePause()
    {
        PauseUI.SetActive(!PauseUI.activeSelf);

        if (PauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    /// <summary>
    /// Method reloading the scene.
    /// </summary>
    public void RetryGame()
    {
        TogglePause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Method loading the main menu.
    /// </summary>
    public void Menu()
    {
        TogglePause();
        SceneManager.LoadScene("MainMenu");
    }
}
