using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] int SCREEN_WIDTH_IN_UNITS = 16;
    [SerializeField] float PADDLE_POS_MAX = 15f;
    [SerializeField] float PADDLE_POS_MIN = 1f;

    // Cached references
    GameSession gameSession;
    Ball ball;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();

        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y)
        {
            x = Mathf.Clamp(GetXPos(), PADDLE_POS_MIN, PADDLE_POS_MAX)
        };
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoplayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * SCREEN_WIDTH_IN_UNITS;
        }
    }
}
