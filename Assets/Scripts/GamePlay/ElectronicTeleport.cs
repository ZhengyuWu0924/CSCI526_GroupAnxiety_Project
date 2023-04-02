using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicTeleport : ElectronicDevice
{
    public Vector3 destination;
    private ElectronicTeleport otherTeleport;

    public override void activateDevice(ElectronicDevice other)
    {
        base.activateDevice(other);
        destination = other.transform.position;
        otherTeleport = (ElectronicTeleport)other;
    }

    public override void disactivateDevice()
    {
        base.disactivateDevice();
    }

    // teleport the player
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && actived)
        {
            GameManager.Instance.player.transform.position = destination;
            
            otherTeleport.actived = false;
            yield return new WaitForSeconds(1.0f);
            otherTeleport.actived = true;
        }
    }
}
