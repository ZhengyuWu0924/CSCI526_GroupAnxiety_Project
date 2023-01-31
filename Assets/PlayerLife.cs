using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Win"))
        {
            Debug.Log("Game Over");
        }
    }

    private void Die()
    {
        //rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey("escape"))
        //{
        //    Application.Quit();
        //}
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
