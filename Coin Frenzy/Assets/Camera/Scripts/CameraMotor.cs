using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private const float OFFSET_X = 0;
    private const float OFFSET_Y = 2;
    private const float OFFSET_Z = -2.4f;
    private const float VERTICAL_OFFSET = 1.35f;
    private const float ROTATION_SPEED = 4.0f;

    public FixedJoystick cameraJoystick;
    public float verticalOffset;

    private Transform playerTransform;
    private Vector3 initialOffset;
    private float rotationAngle;

    private void Start()
    {
        playerTransform = (GameObject.FindGameObjectWithTag("Player")).transform;
        //initialOffset = transform.position - playerTransform.position;
        initialOffset = new Vector3(OFFSET_X, OFFSET_Y, OFFSET_Z);
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        rotationAngle += cameraJoystick.Horizontal * ROTATION_SPEED;
        transform.position = playerTransform.position + Quaternion.AngleAxis(rotationAngle, Vector3.up) * initialOffset;
        transform.rotation = Quaternion.LookRotation(playerTransform.position - transform.position + (Vector3.up * (verticalOffset + cameraJoystick.Vertical)), Vector3.up);
    }
}
