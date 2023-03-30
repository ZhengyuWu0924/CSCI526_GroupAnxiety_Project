using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicPen : BasicPen
{
    //public int cornerVertics;
    //public int capVertics;
    public override void InitializePen()
    {
        base.InitializePen();
        base.penName = "ElectronicPen";
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }

}
