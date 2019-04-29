using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "breakout-like/Create ItemList")]
public class ItemListModel : ScriptableObject
{
    public List<GameObject> items;
    public float itemDropProbability = 0.1f;
}
