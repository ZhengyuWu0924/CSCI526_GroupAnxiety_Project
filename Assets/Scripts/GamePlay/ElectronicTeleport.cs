using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicTeleport : ElectronicDevice
{
    public Vector3 destination;
    private ElectronicTeleport otherTeleport;

    public override void connectDevice(ElectronicDevice other)
    {
        base.connectDevice(other);
        destination = other.transform.position;
        otherTeleport = (ElectronicTeleport)other;
    }

    public override void disconnectDevice()
    {
        base.disconnectDevice();
    }

    // teleport the player
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && connected)
        {
            GameManager.Instance.player.transform.position = destination;
            
            otherTeleport.connected = false;
            yield return new WaitForSeconds(1.0f);
            otherTeleport.connected = true;
        }
    }

    public override DeviceType getDeviceType()
    {
        return DeviceType.TELEPORT;
    }

}
