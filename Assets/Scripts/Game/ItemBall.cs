using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBall : FallenSprite
{
    public override void OnEffect(Player player)
    {
    }

    public override void OnObtained(Player player)
    {
        GameController.instance.SpawnBall(new Vector2(player.transform.position.x, player.transform.position.y + 40), false);
    }
}
