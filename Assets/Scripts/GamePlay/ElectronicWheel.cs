using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicWheel : ElectronicDevice
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

    public float rotationSpeed;
    private Coroutine rotateCoroutine;
    
    private IEnumerator rotateAtSpeed()
    {
        while(true)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }


    public override void deviceStart()
    {
        rotateCoroutine = StartCoroutine(rotateAtSpeed());
    }

    public override void deviceStop()
    {
        StopCoroutine(rotateCoroutine);
    }
}
