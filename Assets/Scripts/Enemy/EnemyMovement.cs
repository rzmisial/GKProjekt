using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    private int waypointIndex = 0;

    public Transform character;
    private Vector3 currentLocation;
    private Vector3 previousLocation;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        character = this.gameObject.transform.GetChild(0);

        enemy.health = enemy.startHealth;
        target = Waypoints.points[0];
        currentLocation = transform.position;
    }

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

    void EndPath()
    {
        if (PlayerStats.Lives > 0)
            PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    private void MovementListener()
    {
        previousLocation = currentLocation;
        currentLocation = transform.position;

    }
}
