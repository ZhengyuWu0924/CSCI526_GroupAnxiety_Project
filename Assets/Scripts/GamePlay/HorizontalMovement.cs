using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public float moveRange = 5f;
    public float moveSpeed = 2f;
    private Vector2 startPos;
    private Rigidbody2D rb;
    private bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            float x = Mathf.PingPong(Time.time * moveSpeed, moveRange);
            transform.position = new Vector2(startPos.x + x, transform.position.y);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Drawn Object")
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }
    }
}
