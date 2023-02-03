using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector3 scaleChange = new Vector3(0.1f, 0.5f, 0);
    public float height = 3.0f;
    private bool isWatered = false; //whether the tree is watered
    public GameObject WoodButton;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WaterPen"))
        {
            Destroy(collision.gameObject);
            Debug.Log("WaterPen");
            isWatered = true;
            WoodButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // The tree grows
        if(isWatered && transform.localScale.y < height)
        {
            transform.localScale += Time.deltaTime * scaleChange;
            transform.position += Time.deltaTime * scaleChange;
        }
    }
}

