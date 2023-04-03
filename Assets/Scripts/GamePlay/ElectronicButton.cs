using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicButton : ElectronicDevice
{
    public bool onlyActivatedOnce;
    private bool isActivate;
    public GameObject objectToDisappear;

    void Start()
    {
        isActivate = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Mutable Object") || collision.gameObject.CompareTag("Player") && actived))
        {
            SpriteRenderer renderer = objectToDisappear.GetComponent<SpriteRenderer>();
            BoxCollider2D collider = objectToDisappear.GetComponent<BoxCollider2D>();
            if (!isActivate)
            {
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.1f);
                collider.enabled = false;
                isActivate = true;
            }
            else
            {
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
                collider.enabled = true;
                isActivate = false;
            }
            
        }
    }

    public override void activateDevice(ElectronicDevice other)
    {
        base.activateDevice(other);
        objectToDisappear = other.transform.gameObject;
    }

    public override void disactivateDevice()
    {
        base.disactivateDevice();
    }
}
