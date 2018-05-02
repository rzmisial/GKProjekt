using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusController : MonoBehaviour {

    public float movementRatio;

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
    }
}
