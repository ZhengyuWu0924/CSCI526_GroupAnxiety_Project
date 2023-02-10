using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBrush : BasicBrush
{
    public override void changeProperties(GameObject gameObject)
    {
        base.changeProperties(gameObject);

        mutableObject.ChangeGravity();
       

    }
}
