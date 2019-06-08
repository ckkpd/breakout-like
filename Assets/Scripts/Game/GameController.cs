using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioSource audioSource;

    public GameObject normalBall;
    public BlockListModel blockList;

    public int ballsLeft = 3;
    public int ballsNum; //current amount of balls
    public int blocksNum; // current amount of blocks
    public int breakableBlocksNum; // current amount of breakable blocks

    public static int score;
    public int ballsLeftScore = 500;

    public ItemListModel itemlist;
    public float itemDropProbability = 0.1f;

    public float fallenItemSpeed = 1f;
    public static bool gameFinished = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log(GetCurrentStage());
    }

    public static void InitializeGame()
    {
        if (!gameFinished)
        {
            throw new System.Exception("The game has not been ended!");
        }
        gameFinished = false;
        score = 0;
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

        if (ballsLeft == 0 && isUser) return false;
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
        if(!gameFinished && ballsLeft == 0 && ballsNum == 0)
        {
            Debug.Log("You lose!");
            UIController.instance.OnStageEnd(false);
        }
    }

    public void AddBlock(Block block)
    {
        blocks.Add(block);
        blocksNum++;
        Debug.Log(block.model.isUnbreakable);
        if (!block.model.isUnbreakable) breakableBlocksNum++;
    }

    public static void AddScore(int score)
    {
        GameController.score += score;
    }
    public void OnBlockDestroyed(Block block)
    {
        if (gameFinished) return;
        float rnd = Random.Range(0f, 1f);
        if(rnd <= itemlist.itemDropProbability)
        {
            Debug.Log("come!");
            int itemrnd = Random.Range(0, itemlist.items.Capacity);
            Instantiate(itemlist.items[itemrnd], block.transform.position, Quaternion.identity);
        }
        breakableBlocksNum--;
        audioSource.PlayOneShot(block.model.soundOnBreak);
        AddScore(block.model.score);
        if(breakableBlocksNum == 0)
        {
            Debug.Log("You won!");
            balls.ForEach(i =>
            {
                if (i != null) Destroy(i.gameObject);
            });
            OnStageEnd();
        }
    }

    public void OnStageEnd()
    {
        score += ballsLeft * ballsLeftScore;
        UIController.instance.OnStageEnd(true);
    }

    public void OnGameEnd()
    {
        InitializeGame();
    }

    public void DebugFunc(string s)
    {
        switch(s)
        {
            case "ballXto0":
                Debug.Log("ballXto0");
                foreach (var i in balls)
                {
                    if (i == null) continue;
                    i.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
                }
                break;
            default:
                break;
        }
    }

    public static int GetCurrentStage()
    {
        return int.Parse(SceneManager.GetActiveScene().name.Substring(5));
    }
}