using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool GameEnded = false;

    public GameObject GameOverUI;


    void Start()
    {
        GameEnded = false;
    }

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

    void EndGame()
    {
        GameEnded = true;

        GameOverUI.SetActive(true);
    }
}
