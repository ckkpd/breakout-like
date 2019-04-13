using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private bool moveRight;
    private bool moveLeft;

    public float baseSpeed = 200f;

    // Start is called before the first frame update
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    private void FixedUpdate()
    {
        float tmpSpeed = baseSpeed;
        if ((moveRight && moveLeft) || (!moveRight && !moveLeft)) tmpSpeed = 0f;
        else if (moveLeft) tmpSpeed *= -1;

        rb2d.velocity = new Vector2(tmpSpeed, rb2d.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
    }
}
