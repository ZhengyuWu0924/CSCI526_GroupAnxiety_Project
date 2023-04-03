using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicButton : ElectronicDevice
{
    private bool isStart;
    public ElectronicDevice otherDevice;

    void Start()
    {
        isStart = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Mutable Object") || collision.gameObject.CompareTag("Player")) && connected)
        {
            if (!isStart)
            {
                otherDevice.deviceStart();
                isStart = true;
            }
            else
            {
                otherDevice.deviceStop();
                isStart = false;
            }
            
        }
    }

    public override void connectDevice(ElectronicDevice other)
    {
        base.connectDevice(other);
        otherDevice = other.gameObject.GetComponent<ElectronicDevice>();
    }

    public override void disconnectDevice()
    {
        base.disconnectDevice();
    }


    public override DeviceType getDeviceType()
    {
        return DeviceType.SOURCE;
    }
}
