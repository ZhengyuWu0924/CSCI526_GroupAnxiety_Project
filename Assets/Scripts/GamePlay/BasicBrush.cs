using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicBrush : MonoBehaviour
{
    protected MutableObject mutableObject;

    public Color brushColor;
    
    public virtual void InitializeBrush()
    {
        
    }


    public virtual void changeProperties(GameObject gameObject)
    {
        mutableObject = gameObject.GetComponent<MutableObject>();
        mutableObject.ChangeColor(brushColor);
    }
}
