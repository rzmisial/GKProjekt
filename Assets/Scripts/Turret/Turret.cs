using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Turret;
using UnityEngine;

/// <summary>
/// Class representing a turret.
/// </summary>
public class Turret : MonoBehaviour, IUpgrading
{
    /// <summary>
    /// transform of the target to shoot at
    /// </summary>
    protected Transform target;

    /// <summary>
    /// Reference to the enemy set as the target.
    /// </summary>
    private Enemy targetEnemy;

    /// <summary>
    /// range of the turret. The turret will only shoot at targets within its range.
    /// </summary>
    [Header("Attributes")]
    public float range = 40f;

    /// <summary>
    /// bullet scheme
    /// </summary>
    [Header("Use Bullets(default)")]
    public GameObject bulletPrefab;

    /// <summary>
    /// time rate at which the turret will fire
    /// </summary>
    public float fireRate = 1f;

    /// <summary>
    /// time remaining until the turret can fire again
    /// </summary>
    protected float fireCountdown = 0f;

    /// <summary>
    /// information on whether or not the turret will use a laser
    /// </summary>
    [Header("Use Laser")]
    public bool useLaser = false;

    /// <summary>
    /// damage dealt when using the laser
    /// </summary>
    public int damageOverTime = 30;

    /// <summary>
    /// ratio at which enemies hit by the laser will slow down
    /// </summary>
    public float slowPct = .5f;

    /// <summary>
    /// reference to a generic line renderer
    /// </summary>
    public LineRenderer lineRenderer;

    /// <summary>
    /// reference to the impact effect prefab
    /// </summary>
    public ParticleSystem impactEffect;

    /// <summary>
    /// enemy tag used to identify enemies
    /// </summary>
    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    /// <summary>
    /// reference to the part that is supposed to be rotated to aim at enemies
    /// </summary>
    public Transform partToRotate;

    /// <summary>
    /// speed at which the rotating part can rotate
    /// </summary>
    public float turnSpeed = 10f;

    /// <summary>
    /// position from which bullets are shot
    /// </summary>
    public Transform firePoint;

    /// <summary>
    /// Initialization method.
    /// </summary>
    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    /// <summary>
    /// Method performing an update every frame. Searches for enemies and shoots.
    /// </summary>
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

    /// <summary>
    /// Rotates the rotating part towards the enemy.
    /// </summary>
    virtual protected void Rotate()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotationLook = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, rotationLook, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    /// <summary>
    /// performes a lser shot at the target
    /// </summary>
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

    /// <summary>
    /// shoots conventional bullets at the target
    /// </summary>
    virtual protected void Shoot()
    {
        GameObject bulletToGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletToGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }

    }

    /// <summary>
    /// Updates the information about the target.
    /// </summary>
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

    /// <summary>
    /// Represents the fire range via gizmos.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    /// <summary>
    /// Performs an turret upgrade.
    /// </summary>
    public void Upgrade()
    {
        turnSpeed += 5f;
        fireRate += 1f;
    }
}
