using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Represents an enemy
/// </summary>
public class Enemy : MonoBehaviour {

    /// <summary>
    /// initial speed
    /// </summary>
    public float startSpeed = 30;

    /// <summary>
    /// current speed
    /// </summary>
    [HideInInspector]
	public float speed;

    /// <summary>
    /// initial health
    /// </summary>
    public float startHealth = 100;
    /// <summary>
    /// current health
    /// </summary>
    public float health;

    /// <summary>
    /// amount of money killing this opponent gives
    /// </summary>
    public int Value = 10;

    /// <summary>
    /// reference to the death visual effect
    /// </summary>
    public GameObject deathEffect;

    /// <summary>
    /// reference to the health bar image
    /// </summary>
	[Header("Unity Stuff")]
	public Image healthBar;

    /// <summary>
    /// information on whether or not the enemy is dead
    /// </summary>
    private bool isDead = false;

    /// <summary>
    /// Initializer method.
    /// </summary>
    void Start()
    {
        speed = startSpeed;
    }

    /// <summary>
    /// Method applying damage to the enemy. If the health falls to 0 or below, the enemy dies.
    /// </summary>
    /// <param name="amount">nymber of points to take from enemy health</param>
    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    /// <summary>
    /// Method slowing the enemy down.
    /// </summary>
    /// <param name="pct">slowing ratio</param>
    public void Slow( float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    /// <summary>
    /// Method responsible for killing the enemy and removing it.
    /// </summary>
    void Die()
    {
        isDead = true;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

		WaveSpawner.EnemiesAlive--;

        PlayerStats.Money += Value;
        Destroy(gameObject);

    }
}
