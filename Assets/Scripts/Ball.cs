using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Params
    [SerializeField] Paddle paddle;
    [SerializeField] float horizontalPush = 0f;
    [SerializeField] float verticalPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] float yVelIncrement = 3f;
   

    // State 
    Vector2 paddleToBallVector;
    bool hasStarted = false;
    public bool touchedBlock = false;
    int collisionCounter = 0;


    // Cached Component References
    AudioSource ballAudioSource;
    Rigidbody2D ballRigidBody2D;
    Paddle paddleReference;

    private void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;

        ballAudioSource = GetComponent<AudioSource>();

        ballRigidBody2D = GetComponent<Rigidbody2D>();

        paddleReference = FindObjectOfType<Paddle>();
    }

    // Update is called once per frame
    void Update()
    {
        AttachBallToPaddle();
        LaunchOnClick();
    }

    private void AttachBallToPaddle()
    {
        if (hasStarted) return;

        Vector2 paddlePos = new Vector2(
            paddle.transform.position.x,
            paddle.transform.position.y
            );

        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnClick()
    {
        if (hasStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalPush, verticalPush);
            hasStarted = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(15f, 0f);
            hasStarted = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(paddleReference);
        Debug.Log(collision.gameObject.name);

        Vector2 velocityTweak = new Vector2(
            Random.Range(-1, 1), 
           0f);

        if(collision.gameObject.name == paddleReference.name)
        {
            Debug.Log("comparou colisão com paddle");
            touchedBlock = false;
        }

        if (hasStarted)
        {
            PlaySoundEffect();
            ballRigidBody2D.velocity += velocityTweak * randomFactor;
            HorizontalLoopSolution();

        }
    }

    private void PlaySoundEffect()
    {
        AudioClip sound = ballSounds[Random.Range(0, ballSounds.Length)];
        ballAudioSource.PlayOneShot(sound);
    }

    public void TouchBlock()
    {
        touchedBlock = true;
    }

    // This is meant to get the ball stuck if ever stuck on an endless horizontal bouncing cycle
    private void HorizontalLoopSolution()
    {
        CheckCollisionVelocityInY();
        if (collisionCounter >= 4)
        {
            collisionCounter = 0;
            IncrementYVelocity();
        }
    }

    private void CheckCollisionVelocityInY()
    {
        if (ballRigidBody2D.velocity.y >= 0.2f)
        {
            collisionCounter++;
        }
    }


    private void IncrementYVelocity()
    {
        Vector2 yVelocityVector = new Vector2(0f, yVelIncrement);
        ballRigidBody2D.velocity += yVelocityVector;
    }
}
