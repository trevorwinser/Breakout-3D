﻿using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;

    [SerializeField] private ScoreCounterUI scoreCounter;
    private int score = 0;

    private int currentBrickCount;
    private int totalBrickCount;

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        // add camera shake here
        currentBrickCount--;
        AddScore(1);
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        if(currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        maxLives--;
        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
        ball.ResetBall();
    }
    
    public void AddScore(int amount)
    {
        score += amount;
        scoreCounter.UpdateScore(score);
    }
}