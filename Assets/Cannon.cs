using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Turret {
    
    public float bulletY;
    public float bulletT;
    public float G;

    public float velocity;
    private float verticalV;
    private float horizontalV;
    private Vector3 direction;

    private Vector3 lastEnemyPosition;

    override protected void Rotate()
    {
        Vector3 direction = target.position- transform.position;
        Quaternion rotationLook = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, rotationLook, Time.deltaTime * turnSpeed).eulerAngles;
        float correction = 2.5f * (range - direction.magnitude) / range;
        partToRotate.rotation = Quaternion.Euler(rotation.x + correction, rotation.y, 0f);
    }
    /*
    override protected void Rotate()
    {
        float distance = (target.position - transform.position).magnitude;

        verticalV = G * bulletT;
        horizontalV = distance/2f / bulletT;

        //bulletVelocity = Mathf.Sqrt(Mathf.Pow(verticalV, 2) + Mathf.Pow(horizontalV, 2));

        float angle = Mathf.Atan(verticalV / horizontalV);

        angle *= Mathf.Rad2Deg;

        direction = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);
        Quaternion rotationLook = Quaternion.LookRotation(direction);
        rotationLook = Quaternion.Euler(-angle, rotationLook.eulerAngles.y, 0);

        partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, rotationLook, Time.deltaTime * turnSpeed);
    }
    */

    override protected void Shoot()
    {
        GameObject bulletToGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        //CannonBall bullet = bulletToGo.GetComponent<CannonBall>();

        if (bulletToGo != null)
        {
            bulletToGo.GetComponent<Rigidbody>().velocity = partToRotate.rotation * Vector3.forward * velocity;
        }

    }
    
}
