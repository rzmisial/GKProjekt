using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for managing enemy waves.
/// </summary>
public class WaveSpawner : MonoBehaviour {

    /// <summary>
    /// number of enemies still alive
    /// </summary>
	public static int EnemiesAlive = 0;
    /// <summary>
    /// array of waves
    /// </summary>
	public Wave[] waves;
    /// <summary>
    /// position at which enemies are to be spawned
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// time spent waitning in between waves
    /// </summary>
	public float timeBetweenWaves = 5;
    /// <summary>
    /// time in seconds to the next wave
    /// </summary>
    private float countdown = 1;
    /// <summary>
    /// current wave index
    /// </summary>
	private int waveIndex = 0;
    /// <summary>
    /// referemce to the wave timer GUI object
    /// </summary>
    public Text waveCountdownText;

    /// <summary>
    ///  reference to thje game manager object
    /// </summary>
    private GameManager gm;

    /// <summary>
    /// Initializer method.
    /// </summary>
    void Start()
    {
        gm = GetComponent<GameManager>();

        if (gm == null)
            print("No game manager.");
        else
            print("Game manager found.");
    }

    /// <summary>
    /// Method responsible for creating new waves when necessary.
    /// </summary>
	void Update()
	{
		if (EnemiesAlive > 0) {
			return;
		}

        if (waveIndex < waves.Length)
        {
            if (countdown <= 0)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }
        }
        else
        {
            Debug.Log("Level won");
            gm.EndGame();
            this.enabled = false;
        }
		countdown -= Time.deltaTime;

	    countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

	    waveCountdownText.text = string.Format("{0:00.00}", countdown);
	}

    /// <summary>
    /// Method generating new enemies within a wave.
    /// </summary>
    /// <returns></returns>
	IEnumerator SpawnWave () 
	{
        PlayerStats.Rounds++;
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
		waveIndex++;

	}

    /// <summary>
    /// Method generating a single enemy.
    /// </summary>
    /// <param name="enemy"></param>
	void SpawnEnemy(GameObject enemy){
		Instantiate (enemy, spawnPoint.position, spawnPoint.rotation);
		EnemiesAlive++;
	}
}
