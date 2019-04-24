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
    public float maximumSpeed = 1000f;
    public float degreeClamp = Mathf.PI / 6;

    public int baseStrength = 1;
    public int strength;

    int blockTouched = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -1).normalized * initialSpeed;

        strength = baseStrength;
    }
    
    void FixedUpdate()
    {
        // あまりにもボールが遅いときはスピードをつけてあげる
        if(rb2d.velocity.magnitude < leastSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * leastSpeed;
        }
        if(rb2d.velocity.magnitude > maximumSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maximumSpeed;
        }

        float magnitude = rb2d.velocity.magnitude;
        float theta = Mathf.Acos(rb2d.velocity.normalized.x);
        theta = Mathf.Clamp(theta, degreeClamp, Mathf.PI - degreeClamp);
        Vector2 vec = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta) * rb2d.velocity.y < 0 ? -1 : 1) * magnitude;

        rb2d.velocity = vec;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            collision.gameObject.GetComponent<Block>().SubstractHP(strength);
            blockTouched++;
        }
    }

    private void OnDestroy()
    {
        GameController.instance.OnBallDestroyed(this);
    }
}
