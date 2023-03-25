using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteInk : MonoBehaviour
{
    public float powerUpDuration = 7f;
    private bool isPoweredUp = false; 
    private float originalInk; 
    private float powerUpEndTime;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collide");
        if (other.gameObject.CompareTag("Player"))
        {
            // The player has picked up the powerup
            //Debug.Log("The player has picked up the powerup");
            isPoweredUp = true;
            originalInk = gameManager.getInk();
            gameManager.setInk(1000000);
            powerUpEndTime = Time.time + powerUpDuration;
            StartCoroutine(PowerUpTimer());
            SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
            BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.0f);
            collider.enabled = false;
        }
    }

    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(powerUpDuration);


        // The powerup has expired
        isPoweredUp = false;
        gameManager.setInk(originalInk);
        Destroy(this.gameObject);
    }
}
