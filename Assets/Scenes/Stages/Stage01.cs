using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stage01 の生成を行います。
/// </summary>
public class Stage01 : MonoBehaviour
{
    public int startX = -300, startY = 100, intervalX = 75, intervalY = 25, endX = 380, endY = -240;

    void Start()
    {
        Generate();
    }
    
    /// <summary>
    /// ステージの生成をします。
    /// </summary>
    void Generate()
    {
        GameObject block = GameController.instance.normalBlock;

        uint cnt = 0;
        for (int i = startX; i < endX; i += intervalX)
        {
            for (int j = startY; j > endY; j -= intervalY)
            {
                cnt++;
                GameObject obj = Instantiate(block, new Vector3(i, j, 0), Quaternion.identity);
                obj.GetComponent<SpriteRenderer>().color = new Color(0.5f - cnt/100f, cnt / 100f, 1 - cnt/100f);
            }
        }
    }
}
