using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Params
    [SerializeField] Paddle paddle;
    [SerializeField] float horizontalPush = 2f;
    [SerializeField] float verticalPush = 15f;
   

    // State 
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    private void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
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
}
