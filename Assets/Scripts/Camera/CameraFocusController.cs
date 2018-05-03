using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusController : MonoBehaviour {

    public float movementRatio;
    public Camera cam;

    private CameraController camControl;

    void Start()
    {
        camControl = cam.GetComponent<CameraController>();
    }

    void LateUpdate()
    {
        if (GameManager.GameEnded)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            transform.position -= new Vector3(movementRatio, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(movementRatio, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(0, 0, movementRatio);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, movementRatio);
        }

        transform.localPosition = new Vector3(transform.localPosition.x > 200 - camControl.distance/2 ? 200 - camControl.distance/2 : transform.localPosition.x,
                                              transform.localPosition.y,
                                              transform.localPosition.z > 500 - camControl.distance ? 500-camControl.distance : transform.localPosition.z);

        transform.localPosition = new Vector3(transform.localPosition.x < camControl.distance - 550 ? camControl.distance - 550 : transform.localPosition.x,
                                              transform.localPosition.y,
                                              transform.localPosition.z < camControl.distance - 500 ? camControl.distance - 500 : transform.localPosition.z);

    }
}
