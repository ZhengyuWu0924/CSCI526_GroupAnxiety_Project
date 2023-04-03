using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteInk : MonoBehaviour
{
    public float powerUpDuration = 7f;
    private bool isPoweredUp = false;
    private float originalInk; 
    private GameManager gameManager;
    private GameObject player;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");

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
        float initialInk = player.GetComponent<PlayerController>().remainInk;
        gameManager.setInk(initialInk);
        gameManager.updateInk(initialInk - originalInk);
        Destroy(this.gameObject);
    }
}
