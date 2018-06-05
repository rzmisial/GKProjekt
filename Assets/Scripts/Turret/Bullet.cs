using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    protected Transform _target;
    public int damage = 50;

    public float explosionRadius = 0f;

    public float Speed = 120f;

    public GameObject impactEffect;

    public void Seek(Transform target)
    {
        _target = target;
    }

	// Update is called once per frame
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

    protected void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

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

    protected void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
}
