using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfiniteInk : MonoBehaviour
{
    public float powerUpDuration = 7f;
    //private bool isPoweredUp = false;
    private float originalInk; 
    private GameManager gameManager;
    private GameObject player;

    //private TextMeshPro countDownText;
    //private float countDownTime;

    [SerializeField] private Sprite statusImage;
    private PlayerStatusController playerStatusController;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        playerStatusController = GameObject.FindObjectOfType<PlayerStatusController>(true);
        //countDownText = GameObject.Find("CountDownText").GetComponent<TextMeshPro>();

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collide");
        if (other.gameObject.CompareTag("Player"))
        {
            // The player has picked up the powerup
            //Debug.Log("The player has picked up the powerup");
            //countDownTime = powerUpDuration;
            //isPoweredUp = true;

            playerStatusController.ActivateStatus(statusImage);
            playerStatusController.ActivateCountDown(powerUpDuration);

            originalInk = gameManager.getInk();
            gameManager.setInk(1000000);
            StartCoroutine(PowerUpTimer());
            SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
            BoxCollider2D collider = this.gameObject.GetComponent<BoxCollider2D>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.0f);
            collider.enabled = false;
        }
    }

    private void Update()
    {
        /*countDownText.transform.position = player.transform.position + new Vector3(1.5f, 1.7f, 0f);
        if (isPoweredUp)
        {
            if (countDownTime > 0)
            {
                countDownTime -= Time.deltaTime;
            }
            double b = System.Math.Round(countDownTime, 1);
            countDownText.text = b.ToString();
            if (countDownTime < 0)
            {
                countDownText.text = "";
            }
        }*/
    }

    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(powerUpDuration);


        // The powerup has expired
        //isPoweredUp = false;
        float initialInk = player.GetComponent<PlayerController>().remainInk;
        gameManager.setInk(initialInk);
        gameManager.updateInk(initialInk - originalInk);
        Destroy(this.gameObject);
    }
}
