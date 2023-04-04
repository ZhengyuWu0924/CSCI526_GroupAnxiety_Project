using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicPulse : ElectronicDevice
{
    public float pulseRate;
    public ElectronicDevice otherDevice;
    private Coroutine repeatCoroutine;

    private IEnumerator repeatStart(float pulseRate)
    {
        while(true)
        {
            otherDevice.deviceStart();
            yield return new WaitForSeconds(pulseRate);
            otherDevice.deviceStop();
            yield return new WaitForSeconds(pulseRate);
        }
    }

    public override void connectDevice(ElectronicDevice other)
    {
        base.connectDevice(other);
        otherDevice = other.gameObject.GetComponent<ElectronicDevice>();
        repeatCoroutine = StartCoroutine(repeatStart(pulseRate));
    }

    public override void disconnectDevice()
    {
        base.disconnectDevice();
        otherDevice = null;
        StopCoroutine(repeatCoroutine);
    }


    public override DeviceType getDeviceType()
    {
        return DeviceType.SOURCE;
    }
}
