using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicDoor : ElectronicDevice
{
    public override void connectDevice(ElectronicDevice other)
    {
        base.connectDevice(other);
    }

    public override void disconnectDevice()
    {
        base.disconnectDevice();
    }

    public override DeviceType getDeviceType()
    {
        return DeviceType.OUTPUT;
    }

    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    public override void deviceStart()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.1f);
        boxCollider.enabled = false;
    }

    public override void deviceStop()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        boxCollider.enabled = true;
    }
}
