using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Turret {
    
    public LayerMask enemyMask;

    private Vector3 [] bezierPoints;

    void Update()
    {
        if (target == null)
        {
            return;
        }

        Rotate();

        if (fireCountdown <= 0f)
        {
            if(ShootAndConfirm())
                fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    override protected void Rotate()
    {

        if (target != null)
        {
            bezierPoints = CalculateBezierPoints();
            Vector3 direction = bezierPoints[1] - bezierPoints[0];
            Quaternion rotationLook = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, rotationLook, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(rotation.x /*+ correction*/, rotation.y, 0f);
        }
    }

    protected bool ShootAndConfirm()
    {
        Vector3 rotation2D = partToRotate.rotation.eulerAngles;
        rotation2D.x = rotation2D.z = 0;
        Quaternion qRotation2D = Quaternion.Euler(rotation2D);
        
        
        Ray ray = new Ray(partToRotate.position, qRotation2D * Vector3.forward);
        if(Physics.SphereCast(ray, 10f, /*out hit,*/ range, enemyMask))
        {
            GameObject bulletToGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletToGo.GetComponent<CannonBall>().BezierPoints = bezierPoints;
            return true;
        }

        return false;

    }

    private Vector3 [] CalculateBezierPoints()
    {
        Vector3[] result = new Vector3[3];

        result[0] = firePoint.position;
        result[2] = target.transform.position;
        result[1] = result[2] * 0.5f + result[0]* 0.5f;


        float point1Distance =  range/2f  * Mathf.Sqrt(2f);

        float h1 = Mathf.Sqrt(Mathf.Pow(point1Distance, 2) - Mathf.Pow((result[1] - result[0]).magnitude, 2) / 4f);
        

        result[1].y = h1;

        return result;
    }

    private Vector3 CalculateBezierPosiiton(float t)
    {
        Vector3 point = new Vector3();

        point.x = bezierPoints[0].x * (t * t) + 2 * bezierPoints[1].x * t * (1f - t) + bezierPoints[2].x * Mathf.Pow(1f - t, 2);
        point.y = bezierPoints[0].y * (t * t) + 2 * bezierPoints[1].y * t * (1f - t) + bezierPoints[2].y * Mathf.Pow(1f - t, 2);
        point.z = bezierPoints[0].z * (t * t) + 2 * bezierPoints[1].z * t * (1f - t) + bezierPoints[2].z * Mathf.Pow(1f - t, 2);


        return point;
    }

    private void OnDrawGizmosSelected()
    {
        if (bezierPoints != null)
        {
            if (bezierPoints.Length == 3)
            {
                Gizmos.color = Color.yellow;
                Gizmos.color = Color.yellow;

                for (int i = 0; i < 3; i++)
                    Gizmos.DrawWireCube(bezierPoints[i], Vector3.one * 3);

                Gizmos.DrawLine(bezierPoints[0], bezierPoints[1]);
                Gizmos.DrawLine(bezierPoints[1], bezierPoints[2]);

                Gizmos.color = Color.white;
                Vector3 prevPoint = CalculateBezierPosiiton(0);
                for (float f = 0.01f; f < 1f; f += 0.01f)
                {
                    Vector3 newPoint = CalculateBezierPosiiton(f);
                    Gizmos.DrawLine(prevPoint, newPoint);
                    prevPoint = newPoint;
                }
            }
        }
    }


}
