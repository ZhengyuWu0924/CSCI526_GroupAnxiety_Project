using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All properties of mutable object that can be changed by brush
/// </summary>
public class MutableObject : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Color originColor;


    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    // Reset to original status
    public void Reset()
    {
        spriteRenderer.color = originColor;
    }

    // Change object color to brush color
    public void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
    }
    
    // Change object gravity
    public void ChangeGravity()
    {
        if(rigidbody2D.gravityScale > 0)
        {
            rigidbody2D.gravityScale = -0.1f;
        }
        else
        {
            rigidbody2D.gravityScale = 1.0f;
            Reset();
        }
    }
}
