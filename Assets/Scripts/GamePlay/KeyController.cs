using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.CompareTag("Player")){
            this.gameObject.SetActive(false);
            SpriteRenderer renderer = door.GetComponent<SpriteRenderer>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
            door.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
