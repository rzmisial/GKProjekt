using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : Bullet

{

    public float maximumY;

    public float velocity { get; set; }
    public Vector3 direction { get; set; }
    public float verticalVelocity { get; set; }

    public float G;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        print("tworze");
    }

    

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * 100);
    }

    /*
    void OnCollisionEnter(Collision col)
    {
        print("jestem");
        if(col.transform.tag == "Enemy")
        {
            _target = col.transform;

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
    }
    */
}