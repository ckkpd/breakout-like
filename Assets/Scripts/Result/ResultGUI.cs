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
        _ = UpdateRanking();
        scoreText.text = $"{GameController.score}点";
    }

    async Task UpdateRanking()
    {
        var rankingContent = await Ranking.LoadRanking(N);
        rankingText.text = rankingContent;

        var scores = await Score.GetScores(N);
        var index = scores.Select(v => v.score).ToList().BinarySearch(GameController.score);
        if (index <= N)
        {
            scoreText.text = scoreText.text + $" {scores.Capacity - index}位";
        } else
        {
            scoreText.text = scoreText.text + $" 圏外({N}位未満)";
        }
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
