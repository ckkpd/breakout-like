using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void OnEffect(Player player);
    void OnObtained(Player player);
}