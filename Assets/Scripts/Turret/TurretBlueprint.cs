using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing a turret scheme.
/// </summary>
[System.Serializable]
public class TurretBluePrint
{
    /// <summary>
    /// Reference to the prefab of the turret to build.
    /// </summary>
    public GameObject Prefab;

    /// <summary>
    /// cost of building the turret
    /// </summary>
    public int Cost;

    /// <summary>
    /// cost of upgrading the turret
    /// </summary>
    public int UpgradeCost;

    /// <summary>
    /// Obtains information on how much money will be given to the player when they sell the turret.
    /// </summary>
    /// <returns>value of the turret on sale</returns>
    public int GetSellAmount()
    {
        return Cost / 2;
    }

}
