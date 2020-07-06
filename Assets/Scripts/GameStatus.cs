using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    // Config Params
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlock = 10;
    [SerializeField] TextMeshProUGUI scoreText;


    // State
    [SerializeField] int gameScore = 0;

    // Cached Reference
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();

        scoreText.text = "Score: " + gameScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToGameScore()
    {
        if (ball.touchedBlock)
            gameScore = gameScore + pointsPerBlock*2;
        else
            gameScore = gameScore + pointsPerBlock;

        scoreText.text = "Score: " + gameScore.ToString();
    }
}
