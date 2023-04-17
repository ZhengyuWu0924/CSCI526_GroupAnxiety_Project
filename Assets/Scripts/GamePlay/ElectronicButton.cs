using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicButton : ElectronicDevice
{
    private bool isStart;
    private GameObject electronicButton;
    public ElectronicDevice otherDevice;

    void Start()
    {
        electronicButton = gameObject;
        isStart = false;
    }

    private void Update()
    {
        if(connected)
        {
            electronicButton.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }else
        {
            electronicButton.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Mutable Object") || collision.gameObject.CompareTag("Player")) && connected)
        {
            if (!isStart)
            {
                otherDevice.deviceStart();
                yield return new WaitForSeconds(0.2f);
                isStart = true;
            }
            else
            {
                otherDevice.deviceStop();
                yield return new WaitForSeconds(0.3f);
                isStart = false;
            }
            
        }
    }

    public override void connectDevice(ElectronicDevice other)
    {
        base.connectDevice(other);
        //this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        otherDevice = other.gameObject.GetComponent<ElectronicDevice>();
    }

    public override void disconnectDevice()
    {
        base.disconnectDevice();
        //this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }


    public override DeviceType getDeviceType()
    {
        return DeviceType.SOURCE;
    }
}
