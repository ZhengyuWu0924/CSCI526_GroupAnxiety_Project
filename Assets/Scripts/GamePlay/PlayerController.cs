using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    public float drainSpeed = 0.5f;
    private float direction = 0f;
    private Rigidbody2D player;
    private bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        isOnGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
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

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Water")
        {
            player.gravityScale = drainSpeed;
            player.AddForce(Vector2.up * 10);
            Debug.Log(player.gravityScale);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Water")
        {
            player.gravityScale = 1;
            Debug.Log(player.gravityScale);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
    }

}
