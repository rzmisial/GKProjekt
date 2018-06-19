using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing enemy waypoints
/// </summary>
public class Waypoints : MonoBehaviour {

    /// <summary>
    /// array of all waypoints positions
    /// </summary>
	public static Transform[] points;

    /// <summary>
    /// Initializer method populating the array of waypoints positions.
    /// </summary>
	void Awake()
	{
		points = new Transform[transform.childCount];
		for (int i = 0; i < points.Length; i++) {
			points[i] = transform.GetChild (i);
		}
	}

}
