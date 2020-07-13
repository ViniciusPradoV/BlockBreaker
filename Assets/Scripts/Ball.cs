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
   

    // State 
    Vector2 paddleToBallVector;
    bool hasStarted = false;
    public bool touchedBlock = false;


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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(paddleReference);
        Debug.Log(collision.gameObject.name);

        Vector2 velocityTweak = new Vector2(0f, 
            Random.Range(0, randomFactor));

        if(collision.gameObject.name == paddleReference.name)
        {
            Debug.Log("comparou colisão com paddle");
            touchedBlock = false;
        }

        if (hasStarted)
        {
            PlaySoundEffect();
            ballRigidBody2D.velocity += velocityTweak;
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
}
