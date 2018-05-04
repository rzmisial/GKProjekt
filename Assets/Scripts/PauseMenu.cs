using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUI;

	void Update () {
	    if (Input.GetKeyDown((KeyCode.Escape)) || Input.GetKeyDown(KeyCode.P))
	    {
	        TogglePause();
	    }
	}

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

    public void RetryGame()
    {
        TogglePause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        TogglePause();
        SceneManager.LoadScene("MainMenu");
    }
}
