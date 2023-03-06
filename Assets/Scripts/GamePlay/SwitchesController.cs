using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchesController : MonoBehaviour
{
    public List<GameObject> objectsToDisappear;
    private bool isOn = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mutable Object") || collision.gameObject.CompareTag("Player"))
        {
            isOn = !isOn;
            foreach (GameObject obj in objectsToDisappear)
            {
                SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();

                // if the switch is on, make the game object transparent and disable the collider
                if (isOn)
                {
                    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0f);
                    collider.enabled = false;
                }
                // if the switch is off, make the game object fully visible and enable the collider
                else
                {
                    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
                    collider.enabled = true;
                }
            }
        }
    }

}
