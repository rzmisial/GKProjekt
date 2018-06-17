using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Turret;
using UnityEngine;

public class Turret : MonoBehaviour, IUpgrading
{

    protected Transform target;
    private Enemy targetEnemy;

    [Header("Attributes")]
    public float range = 40f;

    [Header("Use Bullets(default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    protected float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 30;
    public float slowPct = .5f;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;

    // Use this for initialization
    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    // Update is called once per frame
    void Update() {
        if (target == null)
        {
            
            if (useLaser)
            {
                impactEffect = GetComponentInChildren<ParticleSystem>();
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    if(impactEffect != null)
                    {
                        impactEffect.Stop();
                        impactEffect.Clear();
                    }
                   
                }
            }
            return;
        }

        Rotate();

        if (useLaser)
        {
            Laser();
        }
        else
        {
	        if (fireCountdown <= 0f)
	        {
	            Shoot();
	            fireCountdown = 1f / fireRate;
	        }

	        fireCountdown -= Time.deltaTime;
        }
	}

    virtual protected void Rotate()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotationLook = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, rotationLook, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            if (impactEffect != null)
            {
                impactEffect.Play();
            }
        }
        
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized * .5f;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
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
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
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
