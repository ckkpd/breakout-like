using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "breakout-like/Create Block")]
public class BlockModel : ScriptableObject
{
    public int hp;
    public bool isUnbreakable;
    public int score;
    public int scoreOnTouch;
    public AudioClip soundOnTouch;
    public AudioClip soundOnBreak;
}
