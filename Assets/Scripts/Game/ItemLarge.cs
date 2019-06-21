using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLarge : FallenSprite
{
    public override void OnEffect(Player player)
    {
        
    }

    public override void OnObtained(Player player)
    {
        PlaySound();
        Debug.Log("itemlarge");
    	foreach(var ball in GameController.instance.balls) {
            if (ball == null) continue;
            ball.gameObject.transform.localScale = new Vector3(4, 4);
            ball.leastSpeed += 100;
    	}
    }
}
