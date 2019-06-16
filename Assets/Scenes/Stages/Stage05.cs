using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Stage05 の生成を行います。
/// </summary>
public class Stage05 : MonoBehaviour
{
    public int startX = -300, startY = 100, intervalX = 75, intervalY = 25, endX = 360, endY = -240;
    public GameObject blockParent;

    public float duration = 100.0f;
    
    void Start()
    {
        Generate();
    }

    Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    /// <summary>
    /// ステージの生成をします。
    /// </summary>

    string[] map;
    void Generate()
    {
        map = new string[]
        {
            "#.*.o.#.*",
            ".*.o.#.*.",
            "*.o.#.*.o",
            ".o.#.*.o.",
            "o.#.*.o.#",
            ".#.*.o.#.",
            "#.*.o.#.*",
            ".*.o.#.*.",
            "*.o.#.*.o",
            ".o.#.*.o.",
            "o.#.*.o.#",
            ".#.*.o.#.",
            "#.*.o.#.*",
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

                if (map[i][j] != '#')
                {
                    spawnedBlock.GetComponent<SpriteRenderer>().color = GetRandomColor();
                }

            }
        }
    }
    private void Update()
    {
        float phi = Time.time / duration * 2 * Mathf.PI;
        float amp = Mathf.Cos(phi) * 0.5f + 0.5f;
        Camera.main.backgroundColor = Color.HSVToRGB(amp, 0.8f, 0.5f);
    }
}
