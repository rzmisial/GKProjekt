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
		waveCountdownText.text = Mathf.Floor(countdown).ToString();
	}

	IEnumerator SpawnWave () 
	{
		waveIndex++;
		for (int i = 0; i < waveIndex; i++) {
			SpawnEnemy();
			yield return new WaitForSeconds(0.5f);
		}


	}

	void SpawnEnemy(){
		Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}
}
