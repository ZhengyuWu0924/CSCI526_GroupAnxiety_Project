using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedBoost = 4f; // Amount by which the player's speed will be increased
    public float powerUpDuration = 7f; // Duration of the powerup in seconds

    private bool isPoweredUp = false; // Flag to track if the player has picked up the powerup
    private float originalSpeed; // The original speed of the player
    private float powerUpEndTime; // The time at which the powerup will expire

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collide");
        if (other.gameObject.CompareTag("Player"))
        {
            // The player has picked up the powerup
            //Debug.Log("The player has picked up the powerup");
            isPoweredUp = true;
            originalSpeed = other.gameObject.GetComponent<PlayerController>().moveSpeed;
            other.gameObject.GetComponent<PlayerController>().moveSpeed = originalSpeed * speedBoost;
            powerUpEndTime = Time.time + powerUpDuration;
            StartCoroutine(PowerUpTimer(other.gameObject));
            gameObject.SetActive(false); // Disable the powerup object
        }
    }

    IEnumerator PowerUpTimer(GameObject player)
    {
        while (Time.time < powerUpEndTime)
        {
            yield return null;
        }

        // The powerup has expired
        isPoweredUp = false;
        player.GetComponent<PlayerController>().moveSpeed = originalSpeed;
        Destroy(this.gameObject);
    }
}
