﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public static int EnemiesAlive = 0;
	public Wave[] waves;
	public Transform spawnPoint;

	public float timeBetweenWaves = 5;
	private float countdown = 1;
	private int waveIndex = 0;
	public Text waveCountdownText; 

	void Update()
	{
		if (EnemiesAlive > 0) {
			return;
		}
			

		if (countdown <= 0)
		{
			StartCoroutine (SpawnWave ());
			countdown = timeBetweenWaves;
			return;
		}
		countdown -= Time.deltaTime;

	    countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

	    waveCountdownText.text = string.Format("{0:00.00}", countdown);
	}

	IEnumerator SpawnWave () 
	{
	    PlayerStats.Rounds++;
		Wave wave = waves[waveIndex];

		for (int i = 0; i < wave.count; i++) {
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}
		waveIndex++;

		if (waveIndex == waves.Length) {
			Debug.Log ("Level won");	
			this.enabled = false;
		}

	}

	void SpawnEnemy(GameObject enemy){
		Instantiate (enemy, spawnPoint.position, spawnPoint.rotation);
		EnemiesAlive++;
	}
}
