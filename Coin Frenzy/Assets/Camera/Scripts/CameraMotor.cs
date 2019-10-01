using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public FixedJoystick cameraJoystick;
    public float verticalOffset;

    private Transform playerTransform;
    private Vector3 initialOffset;
    private float rotationAngle;

    private float rotationSpeed = 3.0f;
    private void Start()
    {
        playerTransform = (GameObject.FindGameObjectWithTag("Player")).transform;
        initialOffset = transform.position - playerTransform.position;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        rotationAngle += cameraJoystick.Horizontal * rotationSpeed;
        transform.position = playerTransform.position + Quaternion.AngleAxis(rotationAngle, Vector3.up) * initialOffset;
        transform.rotation = Quaternion.LookRotation(playerTransform.position - transform.position + (Vector3.up * verticalOffset), Vector3.up);
    }
}
