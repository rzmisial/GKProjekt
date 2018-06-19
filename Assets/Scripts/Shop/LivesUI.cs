using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class representing the UI related to the number of lives.
/// </summary>
public class LivesUI : MonoBehaviour
{
    /// <summary>
    /// referemce to the text object
    /// </summary>
    public Text livesText;
	
    /// <summary>
    /// Method changing the displayed text every frame based on the number of lives.
    /// </summary>
	void Update ()
	{
	    livesText.text = string.Format("{0} LIVES", PlayerStats.Lives.ToString());
	}
}
