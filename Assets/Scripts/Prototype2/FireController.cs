using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public Collider2D fireCollider;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "WaterPen(Clone)")
        {
        fireCollider.enabled = false;
       sprite.color = new Color (1, 0, 0, 0); 
       Debug.Log(sprite.color);
        }
        
    }
}
