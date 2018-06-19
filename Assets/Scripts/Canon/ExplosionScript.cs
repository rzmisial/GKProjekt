using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class handling explosions
/// </summary>
public class ExplosionScript : MonoBehaviour {

    /// <summary>
    /// timestamp of the beginning od the explosion
    /// </summary>
    private float startTime;

	/// <summary>
    /// Initializer method
    /// </summary>
	void Start () {
        startTime = Time.time;
	}
	
	/// <summary>
    /// Updater - tests how long the object has been alive and destroys it once its animation is over.
    /// </summary>
	void Update () {
        if (Time.time - startTime >= 0.3f)
            Destroy(gameObject);
	}
}
