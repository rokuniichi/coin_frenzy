using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float GRAVITY_CONST = 15.0f;
    private const float RAY_Y_CONST = 0.2f;
    private const float RAY_DISTANCE = 0.5f;

    public FixedJoystick movementJoystick;
    public FixedButton jumpButton;
    public float speed;
    public float jumpForce;

    private Camera mainCamera;
    private Rigidbody playerRb;
    private Animation anim;
    private CapsuleCollider col;
    private Vector3 velocity;
    private float verticalVelocity;
    private float cameraAngleY;
    private bool jump;
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        col = GetComponent<CapsuleCollider>();
        mainCamera = Camera.main;

    }

    private void Update()
    {
        MoveAndRotatePlayer();
        AnimatePlayer();

    }
    public void MoveAndRotatePlayer()
    {
        // Moving and rotating player
        cameraAngleY = mainCamera.transform.rotation.eulerAngles.y;
        velocity = new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical) * speed * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(cameraAngleY +
        Vector3.SignedAngle(Vector3.forward, velocity.normalized, Vector3.up), Vector3.up);
        velocity = Quaternion.AngleAxis(cameraAngleY, Vector3.up) * velocity;

        if (IsGrounded())
        {
            verticalVelocity = 0;
            if (jumpButton.isPressed)
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        { 
            verticalVelocity -= GRAVITY_CONST * Time.deltaTime;
        }

        playerRb.velocity = new Vector3(velocity.x, verticalVelocity, velocity.z);

    }

    private void AnimatePlayer()
    {
        Vector3 tempVelocity = playerRb.velocity;
        tempVelocity.y = 0;
        float magnitude = tempVelocity.magnitude;
        if (!IsGrounded())
        {
            anim.Play("jump");
        } else if (magnitude > 3.0f)
        {
            anim.CrossFade("run");
        } else if (magnitude > 0.1f)
        {
            anim.CrossFade("walk");
        } else
        {
            anim.CrossFade("idle");
        }

        //Debug.Log(magnitude);
    }

    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(col.bounds.center.x, col.bounds.center.y - col.bounds.extents.y + RAY_Y_CONST, col.bounds.center.z), Vector3.down);
        return Physics.Raycast(groundRay.origin, groundRay.direction, 0.25f);
        //Debug.DrawRay(new Vector3(col.bounds.center.x, col.bounds.center.y - col.bounds.extents.y + RAY_Y_CONST, col.bounds.center.z), Vector3.down, Color.red, 1.0f);
    }
}
