using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gravity pen
/// </summary>
public class GravityBrush : BasicBrush
{
    public override void changeProperties(GameObject gameObject)
    {
        // Firstly, call this function in BasicBrush
        base.changeProperties(gameObject);
        base.brushName = "GravityBrush";

        // Then, do something special to this brush
        mutableObject.ChangeGravity();
    }
}
