using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject door;
    private GameObject player;
    static public int keyNum = 0;

    private void Start()
    {
        player = GameManager.Instance.player;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player")){
            followWithPlayer(player);
            keyNum++;
        }
        else if(collision.gameObject == door)
        {
            door.GetComponent<ElectronicDoor>().deviceStart();
            Destroy(gameObject);
        }
    }

    void followWithPlayer(GameObject player)
    {
        if(player.transform.localScale.x < 0)
        {
            transform.position = player.transform.position + new Vector3(-1.0f - 0.2f * keyNum, 0.0f, 0.0f);
        }
        else
        {
            transform.position = player.transform.position + new Vector3(1.0f + 0.2f * keyNum, 0.0f, 0.0f);
        }
        
        transform.parent = player.transform;
    }

}
