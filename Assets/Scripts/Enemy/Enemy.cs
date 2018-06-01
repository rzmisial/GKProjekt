using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float speed = 30;

	private Transform character;
	private Vector3 currentLocation;
	private Vector3 previousLocation;

	private Transform target;
	private int waypointIndex = 0;

    public float startHealth = 100;
    private float health;

    public int Value = 10;

    public GameObject deathEffect;

	[Header("Unity Stuff")]
	public Image healthBar;

    private bool isDead = false;

    void Start()
	{

		character =  this.gameObject.transform.GetChild (0);

	    health = startHealth;
        target = Waypoints.points [0];
		currentLocation = transform.position;
	}

	void Update()
	{
		MovementListener();
		Vector3 direction = target.position - transform.position;
		transform.Translate (direction.normalized * speed * Time.deltaTime, Space.World);

		var rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (transform.position-previousLocation), 1);
		character.transform.rotation = rotation;


		if (Vector3.Distance (transform.position, target.position) <= 3.0)
		{
			GetNextWaypoint ();
		}
	}

	private void GetNextWaypoint()
	{
		if (waypointIndex >= Waypoints.points.Length - 1) {
			EndPath();
			return;
		}

		waypointIndex++;
		target = Waypoints.points [waypointIndex];
	}

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

		WaveSpawner.EnemiesAlive--;

        PlayerStats.Money += Value;
        Destroy(gameObject);

    }

    void EndPath()
    {
		if(PlayerStats.Lives > 0 )
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
