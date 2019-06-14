using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Stage01 の生成を行います。
/// </summary>
public class Stage01 : MonoBehaviour
{
    public int startX = -300, startY = 100, intervalX = 75, intervalY = 25, endX = 360, endY = -240;
    public GameObject blockParent;

    void Start()
    {
        Generate();
    }

    /// <summary>
    /// ステージの生成をします。
    /// </summary>

    string[] map;
    void Generate()
    {
        map = new string[]
        {
            ".........",
            "..*.*.*..",
            ".........",
            ".........",
            ".........",
            "..*.*.*..",
            ".........",
        };
        GameObject block = GameController.instance.blockList.blockList[0];
        GameObject hardBlock = GameController.instance.blockList.blockList[1];
        GameObject unbreakableBlock = GameController.instance.blockList.blockList[2];

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                GameObject selectedBlock = null;
                switch (map[i][j])
                {
                    case '.':
                        selectedBlock = block;
                        break;
                    case '*':
                        selectedBlock = hardBlock;
                        break;
                    case '#':
                        selectedBlock = unbreakableBlock;
                        break;
                    default: continue;
                }
                int x = startX + j * intervalX;
                int y = startY - i * intervalY;
                GameObject spawnedBlock = Instantiate(selectedBlock, new Vector3(x, y), Quaternion.identity, blockParent.transform);

                if (map[i][j] != 3)
                {
                    spawnedBlock.GetComponent<SpriteRenderer>().color = new Color(0.1f * i, 0.1f * i, 0.1f * i);
                }

            }
        }
    }
}