using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 8f;

    private Rigidbody2D player;
    private bool isOnGround;
    private GameManager gameManager;

    // need to change
    [SerializeField] public GameObject win;
    [SerializeField] public GameObject lose;
    

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isOnGround = true;
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
            lose.SetActive(true);
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
            win.SetActive(true);
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
