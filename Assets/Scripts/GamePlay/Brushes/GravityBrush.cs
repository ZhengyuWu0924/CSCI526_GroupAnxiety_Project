using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gravity pen
/// </summary>
public class GravityBrush : BasicBrush
{
    public override void changeProperties(GameObject gameObject, BrushType currentBrush)
    {
        // Firstly, call this function in BasicBrush
        base.changeProperties(gameObject, currentBrush);
        base.brushName = "GravityBrush";

        // Then, do something special to this brush
        mutableObject.ChangeGravity();
    }
}
