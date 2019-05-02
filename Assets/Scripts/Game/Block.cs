using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基本ブロック
/// </summary>
public class Block : MonoBehaviour
{
    public BlockModel model;
    public int currentHp;

    void Start()
    {
        GameController.instance.AddBlock(this);
        currentHp = model.hp;
    }

    /// <summary>
    /// ブロックにダメージが入ったときに呼ばれます。
    /// </summary>
    /// <param name="n">ブロックに与えるダメージ</param>
    public void SubstractHP(int n)
    {
        Debug.Log(n);
        if (model.isUnbreakable)
        {
            GameController.instance.audioSource.PlayOneShot(model.soundOnTouch);
            return;
        }
        currentHp -= n;
        if (currentHp <= 0) Destroy(this.gameObject);
        else
        {
            GameController.AddScore(model.score);
            GameController.instance.audioSource.PlayOneShot(model.soundOnTouch);
        }
    }

    private void OnDestroy()
    {
        GameController.instance.OnBlockDestroyed(this);
    }
}
