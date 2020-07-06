using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;

    // Cached Reference
    Level level;
    GameStatus gameStatus;
    Ball ball;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        level.CountBreakableBlocks();

        gameStatus = FindObjectOfType<GameStatus>();

        ball = FindObjectOfType<Ball>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBlock();
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        level.CountBrokenBlock();
        gameStatus.AddToGameScore();
        ball.TouchBlock();
        Destroy(gameObject);
    }
}
