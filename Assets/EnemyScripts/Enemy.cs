using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 50;

	private Transform target;
	private int waypointIndex = 0;

	void Start()
	{
		target = Waypoints.points [0];
	}

	void Update()
	{
		Vector3 direction = target.position - transform.position;
		transform.Translate (direction.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance (transform.position, target.position) <= 0.8)
		{
			GetNextWaypoint ();
		}
	}

	private void GetNextWaypoint()
	{
		if (waypointIndex >= Waypoints.points.Length - 1) {
			Destroy (gameObject);
			return;
		}

		waypointIndex++;
		target = Waypoints.points [waypointIndex];
	}
}
