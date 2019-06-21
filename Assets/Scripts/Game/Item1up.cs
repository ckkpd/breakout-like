using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1up : FallenSprite
{
    public override void OnEffect(Player player)
    {
        
    }

    public override void OnObtained(Player player)
    {
        PlaySound();
        GameController.instance.ballsLeft++;
    }
}
