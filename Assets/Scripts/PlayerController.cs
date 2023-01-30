using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rBody;
    public float speed = 3;
    public float jumpforce = 8;
    private bool isGround = false;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        // Move the player
        if (horizontalmove != 0)
        {
            rBody.velocity = new Vector2(horizontalmove * speed, rBody.velocity.y);

        }

        // Turn around
        if (facedirection > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(facedirection * transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }else if(facedirection < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(facedirection * transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGround)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, jumpforce);
            isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }
}
