using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing an object responsible for moving the camera along the X and Z axis.
/// </summary>
public class CameraFocusController : MonoBehaviour {

    /// <summary>
    /// step value for movement
    /// </summary>
    public float movementRatio;
    /// <summary>
    /// camera object reference
    /// </summary>
    public Camera cam;

    /// <summary>
    /// camera controller reference - responsible for moving the actual camera
    /// </summary>
    private CameraController camControl;

    /// <summary>
    /// Initializer method.
    /// </summary>
    void Start()
    {
        camControl = cam.GetComponent<CameraController>();
    }

    /// <summary>
    /// Method responsible for updating the object and obtaining user input.
    /// </summary>
    void LateUpdate()
    {
        if (GameManager.GameEnded)
            return;

        float correctedMovementRatio = movementRatio * camControl.distance * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            transform.position -= new Vector3(correctedMovementRatio, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(correctedMovementRatio, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(0, 0, correctedMovementRatio);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, correctedMovementRatio);
        }

        transform.localPosition = new Vector3(transform.localPosition.x > 200 - camControl.distance/2 ? 200 - camControl.distance/2 : transform.localPosition.x,
                                              transform.localPosition.y,
                                              transform.localPosition.z > 500 - camControl.distance ? 500-camControl.distance : transform.localPosition.z);

        transform.localPosition = new Vector3(transform.localPosition.x < camControl.distance - 550 ? camControl.distance - 550 : transform.localPosition.x,
                                              transform.localPosition.y,
                                              transform.localPosition.z < camControl.distance - 500 ? camControl.distance - 500 : transform.localPosition.z);

    }
}
