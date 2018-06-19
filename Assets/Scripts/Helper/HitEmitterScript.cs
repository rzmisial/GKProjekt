using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing particle emission
/// </summary>
public class HitEmitterScript : MonoBehaviour {

    /// <summary>
    /// time left before the emitter is removed
    /// </summary>
    private float timeLeft;

    /// <summary>
    /// Initializer method. Creates a new particle system.
    /// </summary>
    public void Awake()
    {
        ParticleSystem system = GetComponent<ParticleSystem>();

        // New prop doesnt work :((
        timeLeft = system.startLifetime;
    }

    /// <summary>
    /// Checks if the lifetime of the emitter is over.
    /// </summary>
    public void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
