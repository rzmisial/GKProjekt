using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Turret;
using UnityEngine;

public class Turret : MonoBehaviour, IUpgrading
{

    protected Transform target;

    [Header("Attributes")]
    public float range = 40f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Use this for initialization
    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            return;
        }

        Rotate();

	    if (fireCountdown <= 0f)
	    {
	        Shoot();
	        fireCountdown = 1f / fireRate;
	    }

	    fireCountdown -= Time.deltaTime;
	}

    virtual protected void Rotate()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotationLook = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, rotationLook, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    virtual protected void Shoot()
    {
        GameObject bulletToGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletToGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Upgrade()
    {
        turnSpeed += 5f;
        fireRate += 1f;
    }
}
