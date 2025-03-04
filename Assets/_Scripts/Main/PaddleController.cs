using System;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minBallBounceBackSpeed;
    [SerializeField] private float maxBallBounceBackSpeed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputHandler.Instance.OnMove.AddListener(MovePaddle);
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnMove.RemoveListener(MovePaddle);
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Ball"))
    //     {
    //         Rigidbody ballRb = other.gameObject.GetComponent<Rigidbody>();
    //         Vector3 directionToFire = (ballRb.transform.position - transform.position).normalized;
    //         float angleOfContact = Vector3.Angle(transform.forward, directionToFire);
    //         float returnSpeed = Mathf.Lerp(minBallBounceBackSpeed, maxBallBounceBackSpeed, angleOfContact / 90f);
    //         ballRb.linearVelocity = Vector3.zero;
    //         ballRb.angularVelocity = Vector3.zero;
    //         ballRb.AddForce(directionToFire * returnSpeed, ForceMode.Impulse);
    //     }
    // }

    private void MovePaddle(Vector3 moveDirection)
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
