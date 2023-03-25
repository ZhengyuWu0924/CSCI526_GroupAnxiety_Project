using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedBoost = 4f; 
    public float powerUpDuration = 7f; 

    //private bool isPoweredUp = false;
    private float originalSpeed;    

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collide");
        if (other.gameObject.CompareTag("Player"))
        {
            // The player has picked up the powerup
            //Debug.Log("The player has picked up the powerup");
            //isPoweredUp = true;
            originalSpeed = other.gameObject.GetComponent<PlayerController>().moveSpeed;
            other.gameObject.GetComponent<PlayerController>().moveSpeed = originalSpeed * speedBoost;
            StartCoroutine(PowerUpTimer(other.gameObject));
            SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
            BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.0f);
            collider.enabled = false;
            /*gameObject.SetActive(false); */// Disable the powerup object
        }
    }

    IEnumerator PowerUpTimer(GameObject player)
    {
        yield return new WaitForSeconds(powerUpDuration);
        // The powerup has expired
        //isPoweredUp = false;
        player.GetComponent<PlayerController>().moveSpeed = originalSpeed;
        Destroy(this.gameObject);
    }
}
