using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicBrush : MonoBehaviour
{
    public Color penColor;
    public virtual void InitializeBrush()
    {
        
    }


    public virtual void changeProperties(MutableObject mutableObject)
    {
        mutableObject.ChangeColor(penColor);
    }
}
