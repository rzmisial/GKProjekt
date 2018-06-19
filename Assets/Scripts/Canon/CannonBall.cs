using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe representing a cannon ball
/// </summary>
public class CannonBall : Bullet
{
    /// <summary>
    /// collective time stpent moving
    /// </summary>
    public float travelTime;

    /// <summary>
    /// arrau of bezier points for trajectory
    /// </summary>
    public Vector3[] BezierPoints { get; set; }

    /// <summary>
    /// explosion object reference
    /// </summary>
    public GameObject explosion;

    /// <summary>
    /// timestamp of the beginning of movement
    /// </summary>
    private float startTime;

    /// <summary>
    /// Initializer method
    /// </summary>
    void Start()
    {
        startTime = Time.time;
    }

    /// <summary>
    /// 't' parameter for Bezier calculations
    /// </summary>
    private float bezierT = 1f;

    /// <summary>
    /// Method udpating the position and checking for explosion conditions
    /// </summary>
    void Update()
    {
        bool selfExplode = false;
        if (bezierT < 0f)
        {
            selfExplode = true;
            bezierT = 0f;
        }
        
        transform.position = CalculateBezierPosiiton(bezierT);


        if(selfExplode)
        {
            Explode();
            GameObject exp = Instantiate<GameObject>(explosion, new Vector3(transform.position.x, 0, transform.position.z), new Quaternion());
            exp.transform.localScale = new Vector3(2*explosionRadius, 1, 2*explosionRadius);
            Destroy(gameObject);
            return;
        }

        float bezierTDelta = Time.deltaTime / travelTime;
        bezierT -= bezierTDelta;
    }

    /// <summary>
    /// Method calculating position on Bezier curve.
    /// </summary>
    /// <param name="t">'t' parameter for Bezier calculations</param>
    /// <returns>position on the Bezier curve</returns>
    private Vector3 CalculateBezierPosiiton(float t)
    {
        Vector3 point = new Vector3();

        point.x = BezierPoints[0].x * (t * t) + 2 * BezierPoints[1].x * t * (1f - t) + BezierPoints[2].x * Mathf.Pow(1f - t, 2);
        point.y = BezierPoints[0].y * (t * t) + 2 * BezierPoints[1].y * t * (1f - t) + BezierPoints[2].y * Mathf.Pow(1f - t, 2);
        point.z = BezierPoints[0].z * (t * t) + 2 * BezierPoints[1].z * t * (1f - t) + BezierPoints[2].z * Mathf.Pow(1f - t, 2);


        return point;
    }

    /// <summary>
    /// Method handling collisions. Destroys the ball and applies damage to the enemies in range.
    /// </summary>
    /// <param name="col">information about the collision itself</param>
    void OnCollisionEnter(Collision col)
    {
            _target = col.transform;

            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 5f);

            if (explosionRadius > 0f)
            {
                Explode();
                GameObject exp = Instantiate<GameObject>(explosion, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
                exp.transform.localScale = new Vector3(explosionRadius, 1, explosionRadius);
            }
            else
            {
                Damage(_target);
            }

            Destroy(gameObject);
    }
    
}