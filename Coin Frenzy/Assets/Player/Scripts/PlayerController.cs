using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private const float GRAVITY_CONST = 15.0f;
    private const float RAY_Y_CONST = 0.2f;
    private const float RAY_DISTANCE = 0.5f;
    private const float SPEED = 400.0f;
    private const float SPEED_MODIFIER = 2.0f;
    private const float JUMP_FORCE = 8.0f;
    private const float POWERUP_DURATION = 3.0f;

    public Text scoreText;
    public FixedJoystick movementJoystick;
    public FixedButton jumpButton;
    public GameObject playerModel;
    public AudioClip coinSound;
    public AudioClip victorySound;
    public AudioClip gameOverSound;
    public AudioClip powerUpSound;


    private TrailRenderer trail;
    private Camera mainCamera;
    private AudioSource playerAudio;
    private Rigidbody playerRb;
    private Animator anim;
    private CapsuleCollider col;
    private Coroutine lastPowerupHandler;
    private Vector3 velocity;
    private float verticalVelocity;
    private float cameraAngleY;
    private bool jump;
    private bool gameOver;
    private int coinsCollected;
    private float speedModifier;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        anim = playerModel.GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        trail.enabled = false;
        mainCamera = Camera.main;
        coinsCollected = 0;
        speedModifier = 1;
        CoinCollectHandler();
    }

    private void Update()
    {
        MoveAndRotatePlayer();
        AnimatePlayer();
        if (gameOver) GameOverHandler();
    }
    public void MoveAndRotatePlayer()
    {
        // Moving and rotating player
        cameraAngleY = mainCamera.transform.rotation.eulerAngles.y;
        velocity = new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical) * SPEED * speedModifier * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(cameraAngleY +
        Vector3.SignedAngle(Vector3.forward, velocity.normalized, Vector3.up), Vector3.up);
        velocity = Quaternion.AngleAxis(cameraAngleY, Vector3.up) * velocity;

        if (IsGrounded())
        {
            verticalVelocity = -0.1f;
            if (jumpButton.isPressed)
            {
                verticalVelocity = JUMP_FORCE;
            }
        }
        else
        {
            verticalVelocity -= GRAVITY_CONST * Time.deltaTime;
        }

        playerRb.velocity = new Vector3(velocity.x, verticalVelocity, velocity.z);
        playerModel.transform.position = transform.position;
        playerModel.transform.rotation = transform.rotation;
    }

    private void AnimatePlayer()
    {
        Vector3 tempVelocity = playerRb.velocity;
        tempVelocity.y = 0;
        float magnitude = tempVelocity.magnitude;
        anim.SetFloat("speed_f", magnitude);
        //if (!IsGrounded())
        //{
        //    anim.Play("jump");
        //}
        //else if (magnitude > 3.0f)
        //{
        //    anim.CrossFade("run");
        //}
        //else if (magnitude > 0.1f)
        //{
        //    anim.CrossFade("walk");
        //}
        //else
        //{
        //    anim.CrossFade("idle");
        //}

        //Debug.Log(magnitude);
    }


    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(col.bounds.center.x, col.bounds.center.y - col.bounds.extents.y + RAY_Y_CONST, col.bounds.center.z), Vector3.down);
        return Physics.Raycast(groundRay.origin, groundRay.direction, 0.25f);
        //Debug.DrawRay(new Vector3(col.bounds.center.x, col.bounds.center.y - col.bounds.extents.y + RAY_Y_CONST, col.bounds.center.z), Vector3.down, Color.red, 1.0f);
    }

    void CoinCollectHandler()
    {
        scoreText.text = "Coins: " + coinsCollected;
    }
    IEnumerator PowerupCollectHandler()
    {
        speedModifier = SPEED_MODIFIER;
        trail.enabled = true;
        yield return new WaitForSeconds(POWERUP_DURATION);
        trail.enabled = false;
        speedModifier = 1;
    }


    private void GameOverHandler()
    {
        playerAudio.PlayOneShot(gameOverSound, 1.0f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            playerAudio.PlayOneShot(coinSound, 1.0f);
            coinsCollected++;
            Destroy(collision.gameObject);
            CoinCollectHandler();
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            Destroy(collision.gameObject);

            if (lastPowerupHandler != null)
            {
                StopCoroutine(lastPowerupHandler);
            }

            lastPowerupHandler = StartCoroutine(PowerupCollectHandler());
        }
    }


}
