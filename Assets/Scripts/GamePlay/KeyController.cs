using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public ElectronicDoor door;
    public GameObject player;

    private void Start()
    {
        player = GameManager.Instance.player;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player")){
            followWithPlayer(player);
        }
        else if(door = collision.gameObject.GetComponent<ElectronicDoor>())
        {
            door.deviceStart();
            Destroy(gameObject);
        }

    }

    void followWithPlayer(GameObject player)
    {
        transform.position = player.transform.position + new Vector3(1.0f, 0.0f, 0.0f);
        transform.parent = player.transform;
    }

}
