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
    	foreach(var ball in GameController.instance.balls) {

    	}
    }
}
