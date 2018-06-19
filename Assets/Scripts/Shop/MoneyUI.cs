using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Class representing the UI related to funds owned by the player.
/// </summary>
public class MoneyUI : MonoBehaviour
{
    /// <summary>
    /// referemce to the text object
    /// </summary>
    public Text moneyText;


    /// <summary>
    /// Method changing the displayed text every frame based on the owned funds.
    /// </summary>
    void Update ()
	{
	    moneyText.text = string.Format("$ {0}", PlayerStats.Money.ToString());
	}
}
