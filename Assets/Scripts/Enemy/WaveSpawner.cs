using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public Transform enemyPrefab;
	public Transform spawnPoint;

	public float timeBetweenWaves = 5;
	private float countdown = 1;
	private int waveIndex = 0;
	public Text waveCountdownText; 

	void Update()
	{
		if (countdown <= 0)
		{
			StartCoroutine (SpawnWave ());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;

	    countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

	    waveCountdownText.text = string.Format("{0:00.00}", countdown);
	}

	IEnumerator SpawnWave () 
	{
		waveIndex++;
	    PlayerStats.Rounds++;

		for (int i = 0; i < waveIndex; i++) {
			SpawnEnemy();
			yield return new WaitForSeconds(0.5f);
		}


	}

	void SpawnEnemy(){
		Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}
