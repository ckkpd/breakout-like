using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "breakout-like/Create BlockList")]
public class BlockListModel : ScriptableObject
{
    public List<GameObject> blockList = new List<GameObject>();
}
