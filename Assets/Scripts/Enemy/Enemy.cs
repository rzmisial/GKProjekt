using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 30;

	private Transform target;
	private int waypointIndex = 0;

    public float startHealth = 100;
    private float health;

    public int Value = 10;

    public GameObject deathEffect;

    private bool isDead = false;

    void Start()
	{
	    health = startHealth;
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
			EndPath();
			return;
		}

		waypointIndex++;
		target = Waypoints.points [waypointIndex];
	}

    public void TakeDamage(float amount)
    {
        health -= amount;

        // healthBar.fillAmount = health / startHealth;

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
        PlayerStats.Money += Value;
        Destroy(gameObject);
    }

    void EndPath()
    {
        if(PlayerStats.Lives > 0 )
            PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
