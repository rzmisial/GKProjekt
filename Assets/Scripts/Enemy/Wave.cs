using UnityEngine;

/// <summary>
/// Class representing a new wave
/// </summary>
[System.Serializable]
public class Wave	{
    /// <summary>
    /// enemy to spawn
    /// </summary>
	public GameObject enemy;
    /// <summary>
    /// number of enemies to spawn
    /// </summary>
	public int count;

    /// <summary>
    /// time rate at which new enemies will be spawned
    /// </summary>
	public float rate;
}

