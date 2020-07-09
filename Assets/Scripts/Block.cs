﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // Config Params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // Cached References
    Level level;
    GameSession gameSession;
    Ball ball;

    // State Variables
    [SerializeField] int timesHit; // TODO only serialized for debug
    private void Start()
    {
        CountBreakableBlocks();

        gameSession = FindObjectOfType<GameSession>();

        ball = FindObjectOfType<Ball>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
            level.CountBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit == maxHits)
            DestroyBlock();
        else
        {
            ShowNextBlockSprite(timesHit);
        }
    }

    private void ShowNextBlockSprite(int timesHit)
    {
        int spriteIndex = timesHit - 1;

        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
        PlaySoundOnBlockBreak();
        level.CountBrokenBlock();
        gameSession.AddToGameScore();
        ball.TouchBlock();
        TriggerSparklesVFX(gameObject.transform);
        Destroy(gameObject);

    }

    private void TriggerSparklesVFX(Transform transform)
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

    private void PlaySoundOnBlockBreak()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }
}
