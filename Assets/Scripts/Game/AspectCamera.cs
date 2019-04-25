using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectCamera : MonoBehaviour
{
    public float targetRatio = 16f / 9f;
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        float currentRatio = Screen.width * 1f / Screen.height;
        float ratio = targetRatio / currentRatio;
        float rectX = (1f - ratio) / 2f;
        camera.rect = new Rect(rectX, 0f, ratio, 1f);
    }
}
