using System.Collections;
using System.Collections.Generic;
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
    public readonly uint N = 50;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = $"{GameController.score}点";
        _ = UpdateRanking();
    }

    async Task UpdateRanking()
    {

        var scores = await Score.GetScores(N);
        var rankingContent = Ranking.LoadRanking(scores, N);
        rankingText.text = rankingContent;
        Debug.Log("updateranking");
        var index = scores.Select(v => v.score).ToArray().GetUpperBound(GameController.score);
        Debug.Log("u123456");
        Debug.Log(index);
        Debug.Log(scoreText.text);


        if (index <= N)
        {
            scoreText.text = scoreText.text + $" {index+2}位";
        } else
        {
            scoreText.text = scoreText.text + $" 圏外({N}位未満)";
        }
        Debug.Log("test");
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

            SceneManager.LoadScene("Title");
        }
    }
}
