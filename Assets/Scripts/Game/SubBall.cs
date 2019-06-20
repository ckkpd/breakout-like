using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBall : Ball
{
    private void OnDestroy()
    {
        // Do nothing
    }

    void Start()
    {
        base.Start();
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(Random.Range(0, Mathf.PI)) * 1000, Mathf.Sin(Random.Range(0, Mathf.PI)) * 1000);
    }
}
