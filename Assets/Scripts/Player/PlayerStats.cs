using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing all player statistics.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    /// <summary>
    /// amount of funds the player has
    /// </summary>
    public static int Money;
    /// <summary>
    /// initial amount of money
    /// </summary>
    public int StartMoney = 250;

    /// <summary>
    /// current number of lives
    /// </summary>
    public static int Lives;
    /// <summary>
    /// initial number of lives
    /// </summary>
    public int startLives = 20;
    /// <summary>
    /// number of rounds survived
    /// </summary>
    public static int Rounds = 0;

    /// <summary>
    /// Initializer method.
    /// </summary>
    void Start()
    {
        Money = StartMoney;
        Lives = startLives;
    }
}
