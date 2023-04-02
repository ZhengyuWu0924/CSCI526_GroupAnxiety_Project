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

            if(device1 && device2)
            {
                // two teleports
                if (device1.GetType() == typeof(ElectronicTeleport) && device2.GetType() == typeof(ElectronicTeleport)) validConnection = true;
                // input and output
                if (device1.GetType() == typeof(ElectronicDoor) && device2.GetType() == typeof(ElectronicButton) ||
                   device2.GetType() == typeof(ElectronicDoor) && device1.GetType() == typeof(ElectronicButton)) validConnection = true;

                if(validConnection)
                {
                    device1.activateDevice(device2);
                    device2.activateDevice(device1);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if(device1) device1.disactivateDevice();
        if(device2) device2.disactivateDevice();
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