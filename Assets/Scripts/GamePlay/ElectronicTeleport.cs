using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicTeleport : ElectronicDevice
{
    public Vector3 destination;
    private ElectronicTeleport otherTeleport;
    [SerializeField] Color connectedColor;
    [SerializeField] Color disconnectedColor;

    public override void connectDevice(ElectronicDevice other)
    {
        gameObject.GetComponent<SpriteRenderer>().color = connectedColor;
        base.connectDevice(other);
        destination = other.transform.position;
        otherTeleport = (ElectronicTeleport)other;
    }

    public override void disconnectDevice()
    {
        gameObject.GetComponent<SpriteRenderer>().color = disconnectedColor;
        base.disconnectDevice();
    }

    // teleport the player
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("ElectronicDevice")) && connected)
        {
            if (System.Math.Abs(collision.gameObject.transform.position.x - this.gameObject.transform.position.x) < 2.0)
            {
                collision.gameObject.transform.position = destination;
                otherTeleport.connected = false;
                yield return new WaitForSeconds(0.2f);
                otherTeleport.connected = true;
            }
        }
        if (collision.gameObject.CompareTag("Player") && connected)
        {
            if (System.Math.Abs(collision.gameObject.transform.position.x - this.gameObject.transform.position.x) < 1.6)
            {
                GameManager.Instance.player.transform.position = destination;
                otherTeleport.connected = false;
                yield return new WaitForSeconds(0.3f);
                otherTeleport.connected = true;
            }
        }
        if (collision.gameObject.CompareTag("Mutable Object") && connected)
        {
            float rectWidth = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            float rectHeight = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
            Vector2 rectRightCenter = new Vector3(collision.gameObject.transform.position.x + rectWidth / 2, collision.gameObject.transform.position.y - rectHeight / 2, 0);
            Vector2 rectLeftCenter = new Vector3(collision.gameObject.transform.position.x - rectWidth / 2, collision.gameObject.transform.position.y - rectHeight / 2, 0);
            float rectRightCenterDistance = rectRightCenter.x - collision.gameObject.transform.position.x;
            float rectLeftCenterDistance = collision.gameObject.transform.position.x - rectLeftCenter.x;
            if (System.Math.Abs(collision.gameObject.transform.position.x + rectRightCenterDistance - this.gameObject.transform.position.x) < 2.0 || System.Math.Abs(collision.gameObject.transform.position.x - rectLeftCenter.x - this.gameObject.transform.position.x) < 2.0)
            {
                collision.gameObject.transform.position = destination;
                otherTeleport.connected = false;
                yield return new WaitForSeconds(0.2f);
                otherTeleport.connected = true;
            }
        }
    }
    //private IEnumerator OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Mutable Object") && connected)
    //    {
    //        //if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x < 2.0)
    //        //{
    //        Debug.Log("detect collison");
    //            collision.gameObject.transform.position = destination;
    //            otherTeleport.connected = false;
    //            yield return new WaitForSeconds(0.2f);
    //            otherTeleport.connected = true;
    //        //}
    //    }
    //}

    public override DeviceType getDeviceType()
    {
        return DeviceType.TELEPORT;
    }

}
