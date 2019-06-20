using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScatter : FallenSprite
{
    public GameObject subball;
    private int N = 3;
    public override void OnEffect(Player player)
    {
        throw new System.NotImplementedException();
    }
    public override void OnObtained(Player player)
    {
        Debug.Log("ondebug");
        for(int i = 0; i < N; i++)
        {
            var ball = Instantiate(subball);
            Vector2 pos = player.GetComponent<Rigidbody2D>().position;
            pos.y += 50;
            ball.GetComponent<Rigidbody2D>().position = pos;
        }
    }
}
