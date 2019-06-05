using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Stage02 の生成を行います。
/// </summary>
public class Stage02 : MonoBehaviour
{
    public int startX = -300, startY = 100, intervalX = 75, intervalY = 25, endX = 360, endY = -240;

    void Start()
    {
        Generate();
    }

    /// <summary>
    /// ステージの生成をします。
    /// </summary>

    int[][] map;
    void Generate()
    {
        map = new int[][]
        {
            new int[] {3,3,3,3,3,3,3,3,3},
            new int[] {3,0,0,0,0,0,0,0,3},
            new int[] {3,1,2,1,2,1,2,1,3},
            new int[] {3,0,0,0,0,0,0,0,3},
            new int[] {3,2,1,2,1,2,1,2,3},
            new int[] {3,3,3,3,0,3,3,3,3},
            new int[] {3,1,1,1,2,1,1,1,3},
            new int[] {3,0,0,0,0,0,0,0,3},
            new int[] {3,1,2,1,2,1,2,1,3},
            new int[] {3,0,0,0,0,0,0,0,3},
            new int[] {3,2,1,2,1,2,1,2,3},
            new int[] {3,0,0,0,0,0,0,0,3},
            new int[] {3,2,2,2,2,2,2,2,3}
        };
        GameObject block = GameController.instance.blockList.blockList[0];
        GameObject hardBlock = GameController.instance.blockList.blockList[1];
        GameObject unbreakableBlock = GameController.instance.blockList.blockList[2];

        for(int i = 0; i < map.Length; i++)
        {
            for(int j = 0; j < map[i].Length; j++)
            {
                GameObject selectedBlock = null;
                switch(map[i][j])
                {
                    case 1:
                        selectedBlock = block;
                        break;
                    case 2:
                        selectedBlock = hardBlock;
                        break;
                    case 3:
                        selectedBlock = unbreakableBlock;
                        break;
                    default: continue;
                }
                int x = startX + j * intervalX;
                int y = startY - i * intervalY;
                selectedBlock.GetComponent<SpriteRenderer>().color = 
                Instantiate(selectedBlock, new Vector3(x, y), Quaternion.identity);
            }
        }
    }
}
