using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserBrush : BasicBrush
{
   public override void changeProperties(GameObject gameObject)
    {
        if (gameObject.CompareTag("Drawn Object"))
        {
            Destroy(gameObject);
        }
        // Firstly, call this function in BasicBrush
        //base.changeProperties(gameObject);
        //base.brushName = "GravityBrush";

        //// Then, do something special to this brush
        //mutableObject.ChangeGravity();
    }
}
