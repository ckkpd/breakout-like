using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボールが下に落ちたときの削除処理をします。
/// </summary>
public class BallDestroyer : MonoBehaviour
{
    private GameController gc;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
        }
    }
}
