using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All properties that can be changed by brush
/// </summary>
public class MutableObject : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Reset()
    {
        
    }

    public void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
    }
    
    public void ChangeGravity()
    {
        rigidbody2D.gravityScale = -0.1f;
    }
}
