using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int startingLives = 3; // starting number of lives
    private int maxLives; // track current lives

    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private ScoreCounterUI scoreCounter;

    [SerializeField] private List<GameObject> heartObjects; // list of heart GameObjects (instead of just Images)

    private int currentBrickCount;
    private int totalBrickCount;

    private int score = 0;

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
        maxLives = startingLives; // initialize current lives
        UpdateHeartsUI(); // update heart icons when game starts
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
        if (currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        maxLives--;
        // update lives on HUD here
        UpdateHeartsUI();
        // game over UI if maxLives < 1, then exit to main menu after delay
        if (maxLives <= 0)
        {
            // Save the final score before switching to GameOver scene
            PlayerPrefs.SetInt("PlayerScore", score);
            PlayerPrefs.Save();

            CameraShake.Shake(1, 10);
            SceneHandler.Instance.LoadSpecificScene("GameOver");
        }
        else
        {
            CameraShake.Shake(1, 1);
            ball.ResetBall();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreCounter.UpdateScore(score);
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < heartObjects.Count; i++)
        {
            heartObjects[i].SetActive(i < maxLives);
        }
    }
}
