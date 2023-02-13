using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPen : BasicPen
{
    //public int cornerVertics;
    //public int capVertics;
    //public float gravityRatio;

    public override void InitializePen()
    {
        base.InitializePen();
        base.isStraight = true;
        base.penName = "PlatformPen";
        //lineRenderer.numCornerVertices = cornerVertics;
        //lineRenderer.numCapVertices = capVertics;
    }

    ///// <summary>
    ///// Can only draw straight line, Need to be override!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    ///// </summary>
    ///// <param name="newPoint"></param>
    //public override void AddPoint(Vector2 newPoint)
    //{
        
    //}
}
