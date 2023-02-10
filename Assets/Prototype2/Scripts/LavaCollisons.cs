using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaCollisons : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WaterPen"))
        {
            Destroy(collision.gameObject);
            gameObject.tag = "Untagged";
            spriteRenderer.color = new Color(0.3f, 0.25f, 0.25f);
        }else if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
