using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが操作するバー
/// </summary>
public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private bool moveRight;
    private bool moveLeft;
    private bool pressSpace;

    public float baseSpeed = 300f;
    public float reflexParameter = 10;
    
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        pressSpace = Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        float tmpSpeed = baseSpeed;
        if ((moveRight && moveLeft) || (!moveRight && !moveLeft)) tmpSpeed = 0f;
        else if (moveLeft) tmpSpeed *= -1;

        rb2d.velocity = new Vector2(tmpSpeed, rb2d.velocity.y);

        if (pressSpace)
        {
            GameController.instance.SpawnBall(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 40), true);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            float dx = (float)collision.gameObject.transform.position.x - gameObject.transform.position.x;
            dx *= reflexParameter;
            Rigidbody2D ballRb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            float dy = Mathf.Sqrt(
                Mathf.Pow(ballRb2d.velocity.magnitude, 2) - Mathf.Pow(dx, 2)
            );
            ballRb2d.velocity = new Vector2(dx, dy);
        }
    }
}
