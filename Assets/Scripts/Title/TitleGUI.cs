using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleGUI : MonoBehaviour
{
    public GameObject canvasHowTo;
    public GameObject canvasRanking;
    public Text rankingText;
    public uint scoreCount = 10;

    public void OnClickStart()
    {
        Debug.Log("ButtonStart clicked");
        SceneManager.LoadScene("Stage01");
    }

    public void OnClickRanking()
    {
        Debug.Log("ButtonRanking clicked");
        _ = UpdateRanking();
        Debug.Log(canvasRanking.activeInHierarchy);
        canvasRanking.SetActive(!canvasRanking.activeInHierarchy);
    }

    public async Task UpdateRanking()
    {
        var rankingContent = Ranking.LoadRanking(await Score.GetScores(200), scoreCount);
        rankingText.text = rankingContent;
    }

    public void OnClickHowTo()
    {
        canvasHowTo.SetActive(!canvasHowTo.activeInHierarchy);
    }

    public void OnClickQuit()
    {
        Debug.Log("ButtonQuit clicked");
        Application.Quit();
    }
}
