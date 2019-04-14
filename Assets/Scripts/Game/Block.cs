using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基本ブロック
/// </summary>
public class Block : MonoBehaviour
{
    public int score = 100;
    public int hp = 1;

    void Start()
    {
        GameController.instance.AddBlock(this);
    }

    /// <summary>
    /// ブロックにダメージが入ったときに呼ばれます。
    /// </summary>
    /// <param name="n">ブロックに与えるダメージ</param>
    public void SubstractHP(int n)
    {
        hp -= n;
        Debug.Log(hp);
        if (hp <= 0) Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameController.instance.OnBlockDestroyed(this);
    }
}
