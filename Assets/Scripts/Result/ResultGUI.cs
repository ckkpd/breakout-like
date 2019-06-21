using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultGUI : MonoBehaviour
{
    public Text rankingText;
    public Text scoreText;
    public InputField inputFieldName;
    public Button finishButton;
    public readonly uint N = 200;
    private SynchronizationContext mainContext;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started res");
        scoreText.text = $"{GameController.score}点";
        _ = UpdateRanking();
        mainContext = SynchronizationContext.Current;
    }

    async Task UpdateRanking()
    {
        var scores = await Score.GetScores(N);
        scores.Sort((a, b) => b.score - a.score);
        var rankingContent = Ranking.LoadRanking(scores, N);
        rankingText.text = rankingContent;
        Debug.Log("updateranking");

        ulong rank = 1;
        foreach(var s in scores) {
            if(s.score >= GameController.score) rank++;
            else break;
        }

        Debug.Log("u123456");
        Debug.Log(rank);
        Debug.Log(scoreText.text);

        if (rank <= N)
        {
            Debug.Log("<=");
            mainContext.Post(_ => UpdateGUI(scoreText.text + $" {rank}位"), null);
        } else
        {
            Debug.Log(">");
            mainContext.Post(_ => UpdateGUI(scoreText.text + $" 圏外({N}位未満)"), null);
        }
        Debug.Log("test");
    }

    public void UpdateGUI(string str) {
        scoreText.text = str;
    }

    public void OnClickFinishButton()
    {
        _ = OnClickFinishAsync();
    }

    async Task OnClickFinishAsync()
    {
        var name = inputFieldName.text;
        if(name.Length < 1 || name.Length > 10)
        {
            inputFieldName.text = "";
            inputFieldName.GetComponentInChildren<Text>().text = "!Too long name!";
        } else
        {
            finishButton.interactable = false;
            inputFieldName.interactable = false;
            Score score = new Score(GameController.score, name);

            await Score.Post(score);
            GameController.instance.OnGameEnd();

            SceneManager.LoadScene("Title");
        }
    }
}
