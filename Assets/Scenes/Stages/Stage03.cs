using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Stage03 の生成を行います。
/// </summary>
public class Stage03 : MonoBehaviour
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
            "..33.22..",
            "..22133..",
            "..1.3.1..",
            ".222.222.",
            "..1.1.1..",
            ".222.222.",
            "..1.1.1..",
            ".222.222.",
            "..1.1.1..",
            "...3.3...",
            "..3...3..",
            ".3..3..3.",
        };
        GameObject block = GameController.instance.blockList.blockList[0];
        GameObject hardBlock = GameController.instance.blockList.blockList[1];
        GameObject unbreakableBlock = GameController.instance.blockList.blockList[2];

        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                GameObject selectedBlock = null;
                if (map[i][j] == '.') continue;
                if (map[i][j] == '1') selectedBlock = block;
                else selectedBlock = hardBlock;
                int hp = int.Parse(map[i][j].ToString());
                int x = startX + j * intervalX;
                int y = startY - i * intervalY;
                GameObject spawnedBlock = Instantiate(selectedBlock, new Vector3(x, y), Quaternion.identity, blockParent.transform);

                Block bb = spawnedBlock.GetComponent<Block>();
                Debug.Log(bb.currentHp);
                spawnedBlock.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f + x / 100f, 0.5f);
            }
        }
    }
}
