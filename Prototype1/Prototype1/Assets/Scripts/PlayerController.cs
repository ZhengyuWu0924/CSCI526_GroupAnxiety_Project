using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jump;

    private Rigidbody2D m_playerRb;
    private bool m_onGround;

    // Start is called before the first frame update
    void Start()
    {
        jump = 6f;
        speed = 0.07f;
        m_playerRb = GetComponent<Rigidbody2D>();
        m_onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_playerRb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * speed, ForceMode2D.Impulse);
        if (m_onGround && Input.GetKeyDown(KeyCode.W))
        {
            m_playerRb.AddForce(Vector3.up * jump, ForceMode2D.Impulse);
            m_onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            m_onGround = true;
        }
       
    }
}
