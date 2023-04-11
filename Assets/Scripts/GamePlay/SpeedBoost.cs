using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedBoost : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedBoost = 4f; 
    public float powerUpDuration = 7f;

    private bool isPoweredUp = false;
    private float originalSpeed;

    private TextMeshPro countDownText;
    private GameObject player;
    private float countDownTime;
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collide");
        if (other.gameObject.CompareTag("Player"))
        {
            // The player has picked up the powerup
            //Debug.Log("The player has picked up the powerup");
            //countDownText = other.transform.Find("Canvas").gameObject.transform.Find("CountDownText").gameObject.GetComponent<TextMeshPro>();
            //countDownText = other.transform.Find("CountDownText").gameObject.GetComponent<TextMeshPro>();
            countDownTime = powerUpDuration;
            isPoweredUp = true;
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

    private void Start()
    {
        countDownText = GameObject.Find("CountDownText").GetComponent<TextMeshPro>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        countDownText.transform.position = player.transform.position + new Vector3(2f, 1.7f, 0f);
        if (isPoweredUp)
        {
            if (countDownTime > 0)
            {
                countDownTime -= Time.deltaTime;
            }
            double b = System.Math.Round(countDownTime, 1);
            countDownText.text = b.ToString();
            if(countDownTime < 0)
            {
                countDownText.text = "";
            }
        }
    }

    IEnumerator PowerUpTimer(GameObject player)
    {
        yield return new WaitForSeconds(powerUpDuration);
        // The powerup has expired
        isPoweredUp = false;
        player.GetComponent<PlayerController>().moveSpeed = originalSpeed;
        Destroy(this.gameObject);
    }
}
