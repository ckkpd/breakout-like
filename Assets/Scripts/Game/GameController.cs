using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// ゲームの中で使用されるパラメータの管理をします。
/// </summary>
public class GameController : MonoBehaviour
{
    /* balls spawned in the field. */
    List<Ball> balls = new List<Ball>();
    /* blocks spawned in the field. */
    List<Block> blocks = new List<Block>();

    /// <summary>
    /// 外部から呼び出すときに使うインスタンスです。
    /// </summary>
    public static GameController instance;
    static AudioSource audioSource;
    public static uint currentStageID;

    public GameObject normalBall;

    public int ballsLeft = 3;
    public int ballsNum; // current amount of balls
    public int blocksNum; // current amount of blocks
    public int score;

    public float fallenItemSpeed = 1f;
    private bool gameFinished = false;
    private Tilemap tilemap;

    private void Awake()
    {
    }
    private void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        tilemap = GameObject.FindGameObjectWithTag("Block").GetComponent<Tilemap>();
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemap.HasTile(localPlace)) blocksNum++;
        }
    }
    /// <summary>
    /// ボールを生成します。必ずここから生成してください。
    /// </summary>
    /// <param name="pos">ボールを生成する場所</param>
    /// <param name="isUser">ユーザーが発射しようとしたか</param>
    /// <returns>生成されたかどうか</returns>
    public bool SpawnBall(Vector3 pos, bool isUser)
    {
        // ボールがすでに存在するときに新たなボールを発射することはできない
        if (ballsNum > 0 && isUser) return false;

        if (ballsLeft == 0) return false;
        else
        {
            AddBall(Instantiate(normalBall, pos, Quaternion.identity).GetComponent<Ball>());
            if(isUser) ballsLeft--;
            return true;
        }
    }
    public void AddBall(Ball ball)
    {
        balls.Add(ball);
        ballsNum++;
    }
    
    public void OnBallDestroyed(Ball ball)
    {
        ballsNum--;
        if(!gameFinished && ballsLeft == 0)
        {
            Debug.Log("You lose!");
            UIController.instance.OnGameEnd(false);
        }
    }

    public void AddBlock(Block block)
    {
        blocks.Add(block);
        blocksNum++;
    }
    public void OnBlockDestroyed(Block block)
    {
        blocksNum--;
        score += 100;
        if(blocksNum == 0)
        {
            Debug.Log("You won!");
            gameFinished = true;
            balls.ForEach(i =>
            {
                if (i != null) Destroy(i.gameObject);
            });
            UIController.instance.OnGameEnd(true);
        }
    }
}