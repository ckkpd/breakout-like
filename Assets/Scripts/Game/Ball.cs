﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボール
/// </summary>
public class Ball : MonoBehaviour
{
    
    private Rigidbody2D rb2d;
    public float initialSpeed = 300f;
    public float leastSpeed = 250f;
    public float maximumSpeed = 1000f;

    public int baseStrength = 1;
    public int strength;

    public int blockTouched = 0;

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -1).normalized * initialSpeed;

        strength = baseStrength;
    }

    public void FixedUpdate()
    {
        maximumSpeed += 0.1f;

    }
    public void Update()
    {
        // あまりにもボールが遅いときはスピードをつけてあげる
        if(rb2d.velocity.magnitude < leastSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * leastSpeed;
        }
        if(rb2d.velocity.magnitude > maximumSpeed + blockTouched * 10)
        {
            rb2d.velocity = rb2d.velocity.normalized * (maximumSpeed + blockTouched * 10);
        }
        if(Mathf.Abs(rb2d.velocity.y) < leastSpeed / 5)
        {
            Debug.Log("ga");
            rb2d.velocity = new Vector2(rb2d.velocity.x, leastSpeed / 5 * (rb2d.velocity.y < 0 ? -1.5f : 1));
        }
        if (Mathf.Abs(rb2d.velocity.x) < leastSpeed / 15)
        {
            Debug.Log("ga");
            rb2d.velocity = new Vector2(leastSpeed / 15 * (rb2d.velocity.y < 0 ? -1.5f : 1), rb2d.velocity.y);
        }
        if (rb2d.velocity.magnitude < 1e-6) rb2d.velocity = Vector2.one;

        // 何かの理由でボールが異常な挙動を示したとき、削除する
        if (Vector2.Distance(rb2d.position, Vector2.zero) > 1000) Destroy(this.gameObject);
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            collision.gameObject.GetComponent<Block>().SubstractHP(strength);
            blockTouched++;
            // if (blockTouched >= 100) Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        GameController.instance.OnBallDestroyed(this);
    }
}
