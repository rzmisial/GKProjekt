using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing enemy movement.
/// </summary>
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    /// <summary>
    /// position to which the enemy is moving
    /// </summary>
    private Transform target;
    /// <summary>
    /// next waypoint number
    /// </summary>
    private int waypointIndex = 0;

    /// <summary>
    /// position of the model
    /// </summary>
    public Transform character;

    /// <summary>
    /// current world position
    /// </summary>
    private Vector3 currentLocation;

    /// <summary>
    /// previous world position
    /// </summary>
    private Vector3 previousLocation;

    /// <summary>
    /// reference to the enemy object
    /// </summary>
    private Enemy enemy;

    /// <summary>
    /// Initializer method.
    /// </summary>
    void Start()
    {
        enemy = GetComponent<Enemy>();
        character = this.gameObject.transform.GetChild(0);

        enemy.health = enemy.startHealth;
        target = Waypoints.points[0];
        currentLocation = transform.position;
    }

    /// <summary>
    /// Method responsible for moving the enemy and finding a new target.
    /// </summary>
    void Update()
    {
        MovementListener();
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        var rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - previousLocation), 1);
        character.transform.rotation = rotation;


        if (Vector3.Distance(transform.position, target.position) <= 3.0)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    /// <summary>
    /// Method responsible for obtaining the next waypoint.
    /// </summary>
    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    /// <summary>
    /// Method decreasing the number of player lives if the enemy reaches the end of the path.
    /// </summary>
    void EndPath()
    {
        if (PlayerStats.Lives > 0)
            PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    /// <summary>
    /// Listener method updating the location variables.
    /// </summary>
    private void MovementListener()
    {
        previousLocation = currentLocation;
        currentLocation = transform.position;

    }
}
