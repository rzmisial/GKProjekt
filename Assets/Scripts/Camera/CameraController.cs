using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for controlling the camera object itself.
/// </summary>
public class CameraController : MonoBehaviour {

    /// <summary>
    /// reference to an object responsible for positioning the camera on X and Z axis
    /// </summary>
    public GameObject focusObject;

    /// <summary>
    /// step for controlling camera distance
    /// </summary>
    public float scrollRatio;

    /// <summary>
    /// camera dostamce from the ground
    /// </summary>
    public float distance;

    /// <summary>
    /// Method updating camera position.
    /// </summary>
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
