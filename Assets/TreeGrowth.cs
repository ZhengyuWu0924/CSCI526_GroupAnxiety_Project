using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    //private Animator anim;
    private Rigidbody2D rb;
    public Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        if (collision.gameObject.CompareTag("WaterPen"))
        {
            Destroy(collision.gameObject);
            Debug.Log("WaterPen");
            Grow();
        }
    }

    private void Grow()
    {
        //rb.bodyType = RigidbodyType2D.Static;
        while (transform.localScale.y < 3.4f)
        {
            transform.localScale += Time.deltaTime *  scaleChange;
            transform.position += Time.deltaTime *  scaleChange;
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
    //private void RestartLevel()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}
}
