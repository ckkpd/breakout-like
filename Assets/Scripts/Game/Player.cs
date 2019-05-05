using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーが操作するバー
/// </summary>
public class Player : MonoBehaviour
{`
    private Rigidbody2D rb2d;

    private Vector2 position;
    private Vector2 screenToWorldPosition;
    private float startY;
    private bool pressSpace;

    public float reflexParameter = 10;
    public float clampRange = 340;
    public bool isSub = false;

    /// <summary>
    /// エフェクトの継続時間を保持します。
    /// (ItemID, Duration)の形式です。
    /// </summary>
    private Dictionary<string, float> effectDurations = new Dictionary<string, float>();

    private void Awake()
    {
        startY = gameObject.transform.position.y;
    }
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        pressSpace = Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        position = Input.mousePosition;
        screenToWorldPosition = Camera.main.ScreenToWorldPoint(position);
        screenToWorldPosition.y = startY;
        screenToWorldPosition.x = Mathf.Clamp(screenToWorldPosition.x, -clampRange, clampRange);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, screenToWorldPosition, 0.5f);

        if (pressSpace)
        {
            if(!isSub) GameController.instance.SpawnBall(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 40), true);
        }

        foreach(string key in effectDurations.Keys)
        {
            float value = effectDurations[key];
            value -= Time.fixedDeltaTime;

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
            if(isSub) dy *= -1;
            ballRb2d.velocity = new Vector2(dx, dy);
        }
    }

    public void AddEffect(string effect, float dur)
    {
        if (dur == 0f) return;
        float value;
        if(effectDurations.TryGetValue(effect, out value))
        {
            value += dur;
        }
        else
        {
            effectDurations.Add(effect, dur);
        }
    }
}
