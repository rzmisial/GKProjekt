using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform _target;

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
	        Hittarget();
	        return;
	    }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

    private void Hittarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(effectIns,5f);

        Destroy(gameObject);        
    }
}
