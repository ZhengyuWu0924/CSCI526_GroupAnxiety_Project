using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravityCollisions : MonoBehaviour
{
    public float antiGravityScale = -0.1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb2 = collision.gameObject.GetComponent<Rigidbody2D>();
        if(rb2 != null && collision.gameObject.CompareTag("levelObject"))
        {
            rb2.gravityScale = antiGravityScale;
            Destroy(gameObject);
        }
    }
}
