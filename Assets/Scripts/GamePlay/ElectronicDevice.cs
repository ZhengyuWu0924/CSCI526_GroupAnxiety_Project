using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ElectronicDevice : MonoBehaviour
{


    //[SerializeField] private bool _isElectrified;
    //[SerializeField] private bool _isSource;
    //public bool IsElectrified
    //{
    //    get
    //    {
    //        return _isElectrified;
    //    }

    //    set
    //    {
    //        _isElectrified = value;
    //    }
    //}

    //public bool IsSource 
    //{ 
    //    get
    //    {
    //        return _isSource;
    //    }
    //}


    //private void Start()
    //{
    //    if (IsSource)
    //    {
    //        IsElectrified = true;
    //    }
    //}

    ////private void OnTriggerStay2D(Collider2D collision)
    ////{

    ////    if (collision.gameObject.name == "ElectronicPen(Clone)")
    ////    {
    ////        Debug.Log("Stay in cable...");
    ////        ElectronicPen cable = collision.gameObject.GetComponent<ElectronicPen>();

    ////        // update cable
    ////        if (IsElectrified)
    ////        {
    ////            cable.isElectrified = true;
    ////        }
    ////        else
    ////        {
    ////            cable.isElectrified = false;
    ////        }

    ////        IsElectrified = cable.isElectrified || IsSource;
    ////    }
    ////}

    public GameObject connectNode;
    public bool actived;

    public virtual void activateDevice(ElectronicDevice other)
    {
        actived = true;
    }

    public virtual void disactivateDevice()
    {
        actived = false;
    }

    public void electronicEnable()
    {
        connectNode.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void electronicdisable()
    {
        connectNode.GetComponent<SpriteRenderer>().enabled = false;
    }
}
