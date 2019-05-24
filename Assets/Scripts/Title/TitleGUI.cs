using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGUI : MonoBehaviour
{
    public GameObject canvasHowTo;

    public void OnClickStart()
    {
        Debug.Log("ButtonStart clicked");
        SceneManager.LoadScene("Stage01");
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
