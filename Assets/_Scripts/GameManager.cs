using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform ballAnchor;
    [SerializeField] private float ballLaunchSpeed;
    [SerializeField] private Transform bricksContainer;

    private Rigidbody ballRb;
    private bool isBallActive;
    private int currentBrickCount;
    private int totalBrickCount;

    private void OnEnable()
    {
        ballRb = ball.GetComponent<Rigidbody>();
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void ResetBall()
    {
        ballRb.linearVelocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        ballRb.isKinematic = true;
        ballRb.interpolation = RigidbodyInterpolation.None;
        ball.transform.parent = ballAnchor;
        ball.transform.localPosition = Vector3.zero;
        ball.transform.rotation = Quaternion.identity;
        isBallActive = false;
    }

    private void FireBall()
    {
        if (isBallActive) return;
        ball.transform.parent = null;
        ballRb.isKinematic = false;
        ballRb.AddForce(ball.transform.forward * ballLaunchSpeed, ForceMode.Impulse);
        ballRb.interpolation = RigidbodyInterpolation.Interpolate;
        isBallActive = true;
    }

    public void BrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        // add camera shake here
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        if(currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        maxLives--;
        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
        ResetBall();
    }
}
