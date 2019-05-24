using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    public static IEnumerator GetHighScore(System.Action<Score> callback)
    {
        var request = new UnityWebRequest(urlroot + "/api/v1/highest", UnityWebRequest.kHttpVerbGET);
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
        request.downloadHandler = dH;
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
            callback.Invoke(null);
        }
        else
        {
            if (request.responseCode == 200)
            {
                var score = JsonUtility.FromJson<Score>(request.downloadHandler.text);
                callback.Invoke(score);
            }
            else
            {
                callback.Invoke(null);
            }
        }
        yield return null;
    }
    public static IEnumerator GetScores(System.Action<List<Score>> callback)
    {
        var request = new UnityWebRequest(urlroot + "/api/v1/latest", UnityWebRequest.kHttpVerbGET);
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
        request.downloadHandler = dH;
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        /*while (!request.isDone)
        {
            yield return null;
        }*/
        if (request.isNetworkError)
        {
            Debug.Log(request.error);
            callback.Invoke(null);
        }
        else
        {
            if (request.responseCode == 200)
            {
                Debug.Log(request.downloadHandler.text);
                var scores_ = JsonHelper.ListFromJson<string>(request.downloadHandler.text);
                List<Score> scores = new List<Score>();
                scores_.ForEach(item =>
                {
                    Debug.Log(item);
                    Debug.Log(JsonUtility.FromJson<Score>("{\"score\": 100}"));
                    scores.Add(JsonUtility.FromJson<Score>(item));
                });
                callback.Invoke(scores);
            }
            else
            {
                callback.Invoke(null);
            }
        }
        yield return null;
    }
    public static IEnumerator Post(Score s)
    {
        var request = new UnityWebRequest(urlroot + "/api/v1/add", "POST");
        byte[] bodyraw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(s));
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyraw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log("Status Code(GET): " + request.responseCode);
    }
}

public static class JsonHelper
{
    public static List<T> ListFromJson<T>(string json)
    {
        var newJson = "{ \"list\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.list;
    }

    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> list;
    }
}
