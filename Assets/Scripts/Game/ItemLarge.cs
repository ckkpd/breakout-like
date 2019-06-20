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
        Debug.Log("itemlarge");
    	foreach(var ball in GameController.instance.balls) {
            ball.gameObject.transform.localScale = new Vector3(4, 4);
    	}
    }
}
