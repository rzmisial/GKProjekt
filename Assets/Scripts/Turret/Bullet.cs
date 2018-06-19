using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Class representing a base bullet shot from a turret.
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// target at which the turret shot
    /// </summary>
    protected Transform _target;

    /// <summary>
    /// damage taken by the hit enemy
    /// </summary>
    public int damage = 50;

    /// <summary>
    /// radius of the explosion which follows the hit
    /// enemies within the explosion radius will take damage as well
    /// </summary>
    public float explosionRadius = 0f;

    /// <summary>
    /// speed of the bullet
    /// </summary>
    public float Speed = 120f;

    /// <summary>
    /// reference to the impact visual effect object
    /// </summary>
    public GameObject impactEffect;

    /// <summary>
    /// Sets a particular transform as the new target.
    /// </summary>
    /// <param name="target">transform to set as the new target</param>
    public void Seek(Transform target)
    {
        _target = target;
    }

	/// <summary>
    /// Updates the bullet, moving it towards the target.
    /// </summary>
	void Update () {
	    if (_target == null)
	    {
            Destroy(gameObject);
            return;
	    }

	    Vector3 dir = _target.position - transform.position;

	    float distanceThisFrame = Speed * Time.deltaTime;

	    if (dir.magnitude <= distanceThisFrame)
	    {
	        HitTarget();
	        return;
	    }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

    /// <summary>
    /// Handles a sityation when a target is hit. Peforms potential explosion and deals damage to enemy, destroying the bullet in the process.
    /// </summary>
    protected void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 0.5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(_target);
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Method performing an explosion. Deals damage to the target.
    /// </summary>
    protected void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    /// <summary>
    /// Directly damages the target.
    /// </summary>
    /// <param name="enemy"></param>
    protected void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
}
