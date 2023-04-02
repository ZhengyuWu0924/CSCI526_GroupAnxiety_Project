using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchesController : MonoBehaviour
{
    public bool onlyActivatedOnce;
    private bool isActivate;
    public List<GameObject> objectsToDisappear;
    //private bool isOn = false; 
    public List<bool> isOn;
    // Start is called before the first frame update
    void Start()
    {
        isActivate = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mutable Object") || collision.gameObject.CompareTag("Player"))
        {
            //isOn = !isOn;
            //foreach (GameObject obj in objectsToDisappear)
            //{
            //    SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            //    BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();

            //    if the switch is on, make the game object transparent and disable the collider
            //    if (true)
            //    {
            //        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0f);
            //        collider.enabled = false;
            //    }
            //    if the switch is off, make the game object fully visible and enable the collider
            //    else
            //    {
            //        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
            //        collider.enabled = true;
            //    }
            //}
            if ((onlyActivatedOnce && !isActivate) || !onlyActivatedOnce)
            {
                for (int i = 0; i < objectsToDisappear.Count; i++)
                {
                    isOn[i] = !isOn[i];
                    SpriteRenderer renderer = objectsToDisappear[i].GetComponent<SpriteRenderer>();
                    BoxCollider2D collider = objectsToDisappear[i].GetComponent<BoxCollider2D>();

                    // if the switch is on, make the game object transparent and disable the collider
                    if (isOn[i])
                    {
                        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.1f);
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
            isActivate = true;
        }
    }

}
