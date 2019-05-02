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
        GameObject block = GameController.instance.blockList.blockList[0];
        GameObject hardBlock = GameController.instance.blockList.blockList[1];
        GameObject unbreakableBlock = GameController.instance.blockList.blockList[2];

        uint cnt = 0;
        for (int i = startX; i < endX; i += intervalX)
        {
            cnt++;
            for (int j = startY; j > endY; j -= intervalY)
            {
                GameObject obj = Instantiate(cnt % 2 == 0 ? block : hardBlock, new Vector3(i, j, 0), Quaternion.identity);
                obj.GetComponent<SpriteRenderer>().color = new Color(0.5f - cnt/100f, cnt / 100f, 1 - cnt/100f);
            }
        }
        for(int i = startX; i < endX; i += intervalX)
        {
            Instantiate(unbreakableBlock, new Vector3(i, startY + 100, 0), Quaternion.identity);
        }
    }
}
