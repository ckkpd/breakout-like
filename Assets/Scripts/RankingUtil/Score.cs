using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Score
{
    public static string urlroot = "https://blike-api.ckkpd.xyz";
    public int score;
    public string name;
    public string createdAt;

    public Score(int score, string name)
    {
        this.score = score;
        this.name = name;
    }

    public static async Task<Score> GetHighScore()
    {
        var request = new UnityWebRequest(urlroot + "/api/v1/highest", UnityWebRequest.kHttpVerbGET);
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
        request.downloadHandler = dH;
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        Score score = null;
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                score = JsonUtility.FromJson<Score>(request.downloadHandler.text);
            }
        }
        return score;
    }
    public static async Task<List<Score>> GetScores(uint n = 10)
    {
        var request = new UnityWebRequest(urlroot + "/api/v1/latest", "POST");
        string bodystr = $"{{\"n\": {n}}}";
        Debug.Log(bodystr);
        byte[] bodyraw = Encoding.UTF8.GetBytes(bodystr);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyraw);
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
        request.downloadHandler = dH;
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();
        /*while (!request.isDone)
        {
            yield return null;
        }*/
        List<Score> scores = null;
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                Debug.Log(request.downloadHandler.text);
                var scores_ = JsonHelper.ListFromJson<Score>(request.downloadHandler.text);
                scores = scores_;
            }
        }
        return scores;
    }
    public static async Task Post(Score s)
    {
        var request = new UnityWebRequest(urlroot + "/api/v1/add", "POST");
        Debug.Log(JsonUtility.ToJson(s));
        byte[] bodyraw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(s));
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyraw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        Debug.Log("Status Code(POST): " + request.responseCode);
        Debug.Log(request.downloadHandler.text);
    }
}

public static class JsonHelper
{
    public static List<T> ListFromJson<T>(string json)
    {
        var newJson = "{ \"list\": " + json + "}";
        Debug.Log(newJson);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.list;
    }

    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> list;
    }
}
