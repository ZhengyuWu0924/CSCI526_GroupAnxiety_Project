using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCanvas : BasicCanvas
{
    public override void InitializeCanvas()
    {
        base.InitializeCanvas();
        transform.localScale += new Vector3(2, 2, 0);
    }
    
    
}
