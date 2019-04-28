using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        // 何かの理由でボールが異常な挙動を示したとき、削除する
        if (Vector2.Distance(rb2d.position, Vector2.zero) > 1000) Destroy(this.gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider);
        if(collision.gameObject.CompareTag("Block"))
        {
            foreach (ContactPoint2D point in collision.contacts) {
                Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();
                tilemap.SetTile(tilemap.WorldToCell(point.point), null);
                TileBase tile = tilemap.GetTile(tilemap.WorldToCell(point.point));
                GameController.instance.OnBlockDestroyed(null);
                // TODO: TileごとにBlockのComponentをあてたい
            }
        }
    }

    private void OnDestroy()
    {
        GameController.instance.OnBallDestroyed(this);
    }
}
