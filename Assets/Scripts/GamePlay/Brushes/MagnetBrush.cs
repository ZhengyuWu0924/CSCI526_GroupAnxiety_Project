using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Magnetism {Postive, Negtive, None };


public class MagnetBrush : BasicBrush
{
    public Color positiveColor;
    public Color negtiveColor;

    private Magnetism magnetism = Magnetism.None;
    
    /*
        New version magnet properties change function
        Split the original magnet brush to positive brush and negative brush
    */
    public override void changeProperties(GameObject gameObject, BrushType currentBrush){
        // set the current brush name to base brush
        base.brushName = "MagnetBrush";

        // get the mutableObject that cursor interacts with
        mutableObject = gameObject.GetComponent<MutableObject>();

        // get the current magnetism, passed from DrawingTool.cs by detecting pressed button
        magnetism = currentBrush == BrushType.MAGNET_POS ? Magnetism.Postive : Magnetism.Negtive;

        // set the brush color corrensponding to the magnetsim
        brushColor = magnetism == Magnetism.Postive ? positiveColor : negtiveColor;


        // if the current mutable object already has non-none magnetism
        // change it back to default state and reset the color
        // otherwise, give it the current magnetism and related color
        if (mutableObject.magnetism == magnetism){
            mutableObject.ResetColor();
            mutableObject.magnetism = Magnetism.None;
            return;
        } else {
            mutableObject.magnetism = magnetism;
            // brush the mutable obejct with related color
            mutableObject.ChangeColor(brushColor);
        }
    }

    /*
        Old version
    */
    // public override void changeProperties(GameObject gameObject)
    // {
    //     base.brushName = "MagnetBrush";
    //     mutableObject = gameObject.GetComponent<MutableObject>();
    //     if (magnetism == Magnetism.Postive)
    //     {
    //         // Change color
    //         brushColor = positiveColor;
    //         mutableObject.ChangeColor(brushColor);

    //         // If it is a magnet object, set back to normal
    //         if (mutableObject.magnetism != Magnetism.None)
    //         {
    //             mutableObject.ResetColor();
    //             mutableObject.magnetism = Magnetism.None;
    //             return;
    //         }

    //         // Change mutable object's magnetism
    //         mutableObject.magnetism = Magnetism.Postive;

    //         magnetism = Magnetism.Negtive;
    //     }
    //     else if(magnetism == Magnetism.Negtive)
    //     {
    //         brushColor = negtiveColor;
    //         mutableObject.ChangeColor(brushColor);

    //         // If it is a magnet object, set back to normal
    //         if (mutableObject.magnetism != Magnetism.None)
    //         {
    //             mutableObject.ResetColor();
    //             mutableObject.magnetism = Magnetism.None;
    //             return;
    //         }

    //         // Change mutable object's magnetism
    //         mutableObject.magnetism = Magnetism.Negtive;

    //         magnetism = Magnetism.Postive;
    //     }
    // }
}
