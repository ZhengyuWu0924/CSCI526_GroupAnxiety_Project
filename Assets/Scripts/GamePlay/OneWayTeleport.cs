using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayTeleport : MonoBehaviour
{
    private Vector3 destination;
    private bool isActivated;
    public GameObject teleportDestination;
    //public float downwardShift = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        //destination.y -= downwardShift;
        destination = teleportDestination.transform.position;
        isActivated = true;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isActivated)
        {
            //GameManager.Instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GameManager.Instance.player.transform.position = destination;
            isActivated = false;
            //When we need two way transport:
            //teleportDestination.GetComponent<OneWayTeleport>().isActivated = false;
            yield return new WaitForSeconds(5.0f);
            isActivated = true;
            //teleportDestination.GetComponent<OneWayTeleport>().isActivated = true;
        }
    }
}
