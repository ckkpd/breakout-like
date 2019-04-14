using System.Collections;
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
    public float amplify = 0.01f;

    public int baseStrength = 1;
    public int strength;

    int blockTouched = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -1).normalized * initialSpeed;

        strength = baseStrength;
    }
    
    void Update()
    {
        // あまりにもボールが遅いときはスピードをつけてあげる
        if(rb2d.velocity.magnitude < leastSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * leastSpeed;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            collision.gameObject.GetComponent<Block>().SubstractHP(strength);
            blockTouched++;
            rb2d.AddForce(new Vector2(0, amplify));
        }
    }

    private void OnDestroy()
    {
        GameController.instance.OnBallDestroyed(this);
    }
}
