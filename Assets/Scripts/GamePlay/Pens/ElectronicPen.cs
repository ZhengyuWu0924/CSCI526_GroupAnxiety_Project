using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//public class ElectronicPen : BasicPen
//{
//    private HashSet<ElectronicNode> electronicNodes = new HashSet<ElectronicNode>();
//    private ElectronicNode electronicNode;
//    private ElectronicNode electronicNode;
//    private const float EPSILON = 0.000001f;
//    public bool isElectrified = false;

//    public override void InitializePen()
//    {
//        base.InitializePen();
//        base.penName = "ElectronicPen";
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("ElectronicNode"))
//        {

//        }
//    }


//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("ElectronicNode"))
//        {
//            electronicNode = collision.gameObject.GetComponent<ElectronicNode>();
//            electronicNodes.Add(electronicNode);

//            if (electronicNode.IsElectrified)
//            {
//                isElectrified = true;
//            }

//            if (isElectrified)
//            {
//                foreach (ElectronicNode electronicNode in electronicNodes)
//                {
//                    electronicNode.IsElectrified = true;
//                }
//            }
//        }
//    }

//    private void OnDestroy()
//    {
//        foreach (ElectronicNode electronicNode in electronicNodes)
//        {
//            Debug.Log("update node");
//            electronicNode.IsElectrified = electronicNode.IsSource;
//            electronicNode.transform.position += new Vector3(EPSILON, EPSILON, 0);
//        }
//    }
//}

public class ElectronicPen : BasicPen
{
    ElectronicDevice device1;
    ElectronicDevice device2;
    private bool validConnection = false;

    public override void InitializePen()
    {
        base.InitializePen();
        base.penName = "ElectronicPen";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("connectNode"))
        {
            ElectronicDevice electronicDevice = collision.transform.parent.GetComponent<ElectronicDevice>();

            if (device1 == null) device1 = electronicDevice;
            else if (electronicDevice != device1 && device2 == null)
            {
                device2 = collision.transform.parent.GetComponent<ElectronicDevice>();
            }

            if(device1 && device2 )
            {
                // two teleports
                if (device1.getDeviceType() == DeviceType.TELEPORT && device2.getDeviceType() == DeviceType.TELEPORT) validConnection = true;
                // input and output
                if ((device1.getDeviceType() == DeviceType.OUTPUT && device2.getDeviceType() == DeviceType.SOURCE) ||
                   (device2.getDeviceType() == DeviceType.OUTPUT && device1.getDeviceType() == DeviceType.SOURCE)) validConnection = true;

                if (validConnection && !device1.connected && !device2.connected)
                {
                    device1.connectDevice(device2);
                    device2.connectDevice(device1);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if(device1) device1.disconnectDevice();
        if(device2) device2.disconnectDevice();
    }

    public void electronicEnable()
    {
        GetComponent<LineRenderer>().enabled = true;
    }

    public void electronicdisable()
    {
        GetComponent<LineRenderer>().enabled = false;
    }

}