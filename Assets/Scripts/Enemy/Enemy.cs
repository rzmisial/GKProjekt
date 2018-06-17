using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float startSpeed = 30;

    [HideInInspector]
	public float speed;

    public float startHealth = 100;
    public float health;

    public int Value = 10;

    public GameObject deathEffect;

	[Header("Unity Stuff")]
	public Image healthBar;

    private bool isDead = false;


    void Start()
    {
        speed = startSpeed;
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

    public void Slow( float pct)
    {
        speed = startSpeed * (1f - pct);
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
}
