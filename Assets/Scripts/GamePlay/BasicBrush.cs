using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Baisc class of brush
/// All general functions and variables of brush should be in this basic class
/// </summary>
public abstract class BasicBrush : MonoBehaviour
{
    protected MutableObject mutableObject;
    [SerializeField] public string brushName;

    public Color brushColor;

    public Texture2D cursor;

    public int brushCost;

    // Called by DrawingTool.cs, change current object's propertiess
    public virtual void changeProperties(GameObject gameObject, BrushType currentBrush)
    {
        mutableObject = gameObject.GetComponent<MutableObject>();
        mutableObject.ChangeColor(brushColor);
    }

    public virtual Color getColor(BrushType currentBrush){
        return brushColor;
    }
}
