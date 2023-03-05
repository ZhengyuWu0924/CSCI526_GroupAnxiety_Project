using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;
    public float remainInk = 100f;

    private Rigidbody2D player;
    private bool isOnGround;
    private GameManager gameManager;

    private GameObject levelUI;
    private GameObject victoryScreen;
    private GameObject loseScreen;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        player = gameManager.player.GetComponent<Rigidbody2D>();
        isOnGround = true;

        gameManager.setInk(remainInk);

        levelUI = gameManager.levelUI;
        victoryScreen = levelUI.transform.Find("VictoryScreen").gameObject;
        loseScreen = levelUI.transform.Find("LoseScreen").gameObject;
    }

    
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * moveSpeed, player.velocity.y);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * moveSpeed, player.velocity.y);
        }    
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            isOnGround = false;
        }

        if (gameManager.getInk() <= 0)
        {
            loseScreen.SetActive(true);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Win")
        {
            victoryScreen.SetActive(true);
        }
        else if(collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (collision.tag == "MainMenu")
        {
            SceneManager.LoadScene(0);
        }
    }

}
