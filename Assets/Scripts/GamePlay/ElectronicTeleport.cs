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
        if ((collision.gameObject.CompareTag("Mutable Object") || collision.gameObject.CompareTag("ElectronicDevice")) && connected)
        {
            if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x < 2.0)
            {
                collision.gameObject.transform.position = destination;
                otherTeleport.connected = false;
                yield return new WaitForSeconds(0.2f);
                otherTeleport.connected = true;
            }
        }
        if (collision.gameObject.CompareTag("Player") && connected)
        {
            if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x < 1.6)
            {
                GameManager.Instance.player.transform.position = destination;
                otherTeleport.connected = false;
                yield return new WaitForSeconds(0.3f);
                otherTeleport.connected = true;
            }
        }
    }

    public override DeviceType getDeviceType()
    {
        return DeviceType.TELEPORT;
    }

}
