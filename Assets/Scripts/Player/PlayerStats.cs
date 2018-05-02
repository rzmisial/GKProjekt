using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int Money;
    public int StartMoney = 250;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds = 0;

    void Start()
    {
        Money = StartMoney;
        Lives = startLives;
    }
}
