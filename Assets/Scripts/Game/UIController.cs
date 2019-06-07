using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム中のUIの管理をします。
/// </summary>
public class UIController : MonoBehaviour
{
    public Canvas gameCanvas;
    public Canvas resultCanvas;
    public static UIController instance;

    public Text gameText;
    public Text resultText;
    public Text resultDetailText;
    public Button nextButton;
    public Button finishButton;

    public string gameTextFormat = "残弾 {0}つ\n残りブロック {1}個\n{2:0000000}点";

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        resultCanvas.enabled = false;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        gameText.text = string.Format(gameTextFormat, GameController.instance.ballsLeft, GameController.instance.breakableBlocksNum, GameController.score);
    }

    string GetResultDetailString()
    {
        var ins = GameController.instance;
        return $"残っている球 {ins.ballsLeft} × {ins.ballsLeftScore} = {ins.ballsLeft * ins.ballsLeftScore}\n" +
            $"Score: {GameController.score - ins.ballsLeft * ins.ballsLeftScore} + {ins.ballsLeft * ins.ballsLeftScore} = {GameController.score}";
    }

    /// <summary>
    /// 結果画面を表示します。
    /// </summary>
    /// <param name="won">ゲームに買ったかどうか</param>
    public void OnStageEnd(bool won)
    {
        // ゲームが終わったら、ゲーム中に表示していたUIを隠す
        gameCanvas.enabled = false;
        // そして結果のUIを表示する
        resultCanvas.enabled = true;

        Cursor.visible = true;

        if(won)
        {
            resultText.text = "You Won!";
        } else
        {
            resultText.text = "You Lose!";
            nextButton.interactable = false;
        }

        resultDetailText.text = GetResultDetailString();
    }

    public void OnClickNext() => SceneManager.LoadScene(string.Format("Stage{0:00}", GameController.GetCurrentStage() + 1));
    public void OnClickExit() => SceneManager.LoadScene("Result");
}
