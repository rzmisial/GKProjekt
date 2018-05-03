using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject focusObject;
    public float scrollRatio;
    
    [HideInInspector] public float distance { get; set; }

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
        }
        distance = distance < 80 ? 80 : distance;
        distance = distance > 380 ? 380 : distance;
        transform.position = transform.rotation * new Vector3(0, 0, -distance) + focusObject.transform.position;

    }
}
