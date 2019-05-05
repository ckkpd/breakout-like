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
        if(rb2d.velocity.magnitude > maximumSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maximumSpeed;
        }
        if(Mathf.Abs(rb2d.velocity.x) < leastSpeed / 10)
        {
            rb2d.velocity = new Vector2(leastSpeed / 10 * rb2d.velocity.x < 0 ? -1 : 1, rb2d.velocity.y);
        }
        if(Mathf.Abs(rb2d.velocity.y) < leastSpeed / 10)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, leastSpeed / 10 * rb2d.velocity.y < 0 ? -1 : 1);
        }
        if (rb2d.velocity.magnitude == 0) rb2d.velocity = Vector2.one;

        // 何かの理由でボールが異常な挙動を示したとき、削除する
        if (Vector2.Distance(rb2d.position, Vector2.zero) > 1000) Destroy(this.gameObject);
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
