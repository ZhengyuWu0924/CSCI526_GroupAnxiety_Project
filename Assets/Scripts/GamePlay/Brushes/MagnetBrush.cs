using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Magnetism {Postive, Negtive, None };


public class MagnetBrush : BasicBrush
{
    public Color positiveColor;
    public Color negtiveColor;

    private Magnetism magnetism = Magnetism.Postive;
    

    public override void changeProperties(GameObject gameObject)
    {
        base.brushName = "MagnetBrush";
        mutableObject = gameObject.GetComponent<MutableObject>();
        if (magnetism == Magnetism.Postive)
        {
            // Change color
            brushColor = positiveColor;
            mutableObject.ChangeColor(brushColor);

            // If it is a magnet object, set back to normal
            if (mutableObject.magnetism != Magnetism.None)
            {
                mutableObject.ResetColor();
                mutableObject.magnetism = Magnetism.None;
                return;
            }

            // Change mutable object's magnetism
            mutableObject.magnetism = Magnetism.Postive;

            magnetism = Magnetism.Negtive;
        }
        else if(magnetism == Magnetism.Negtive)
        {
            brushColor = negtiveColor;
            mutableObject.ChangeColor(brushColor);

            // If it is a magnet object, set back to normal
            if (mutableObject.magnetism != Magnetism.None)
            {
                mutableObject.ResetColor();
                mutableObject.magnetism = Magnetism.None;
                return;
            }

            // Change mutable object's magnetism
            mutableObject.magnetism = Magnetism.Negtive;

            magnetism = Magnetism.Postive;
        }
    }
}
