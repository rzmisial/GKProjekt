using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject focusObject;

    public float scrollRatio;

    private float distance;

    void Start()
    {
        distance = 40.0f;
    }

    void LateUpdate()
    {

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            distance += scrollRatio;

        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            distance -= scrollRatio;

            if (distance < -100.0f)
                distance = -100.0f;
        }

        transform.position = transform.rotation * new Vector3(0, 0, -distance) + focusObject.transform.position; ;
    }
}
