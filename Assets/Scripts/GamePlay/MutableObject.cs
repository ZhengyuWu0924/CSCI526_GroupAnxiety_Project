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

    public Magnetism magnetism = Magnetism.None;
    public float magnetFactor = 10000.0f;
    public float maxMagnet = 50000.0f;

    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originColor = Color.white;
    }

    

    List<MutableObject> otherMO = new List<MutableObject>();

    private void FixedUpdate()
    {
        if(magnetism != Magnetism.None)
        {
            for (int i = 0; i < otherMO.Count; i++)
            {
                MutableObject mo = otherMO[i];
                if(mo.magnetism == Magnetism.None)
                {
                    continue;
                }

                if(mo.magnetism == magnetism)
                {
                    
                    Vector3 distance = mo.transform.position - transform.position;
                    Vector2 force = (Vector2)distance.normalized * Time.deltaTime * magnetFactor * rigidbody2D.mass * mo.rigidbody2D.mass / Mathf.Pow(distance.magnitude, 2);
                    if (force.magnitude >= maxMagnet) force = force.normalized * maxMagnet;
                    //Debug.Log("Force:" + force.x + "," + force.y);
                    mo.rigidbody2D.AddForce(force,ForceMode2D.Force);
                    
                }
                else
                {
                    Vector3 distance = transform.position - mo.transform.position;
                    Vector2 force = (Vector2)distance.normalized * Time.deltaTime * magnetFactor * rigidbody2D.mass * mo.rigidbody2D.mass / Mathf.Pow(distance.magnitude, 2);
                    if (force.magnitude >= maxMagnet) force = force.normalized * maxMagnet;
                    //Debug.Log("Force:" + force.x + "," + force.y);
                    mo.rigidbody2D.AddForce(force, ForceMode2D.Force);
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        MutableObject mo = other.GetComponent<MutableObject>();
        if (mo)
        {
            if (!otherMO.Contains(mo))
            {
                // add other mutable objects to list
                otherMO.Add(mo);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        MutableObject mo = other.GetComponent<MutableObject>();
        if (mo)
        {
            if (otherMO.Contains(mo))
            {
                // romove other mutable objects to list
                otherMO.Remove(mo);
            }
        }
    }

    // Reset to original status
    public void ResetColor()
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
            ResetColor();
        }
    }
}
