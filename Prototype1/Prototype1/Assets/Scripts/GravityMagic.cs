using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMagic : MonoBehaviour
{
    private Rigidbody woodRb;
    private Rigidbody stoneRb;
    private int inverseVal = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject magicObject = collision.gameObject;
        if (magicObject.CompareTag("MagicObject"))
        {
            magicObject.GetComponent<Rigidbody2D>().gravityScale = -0.1f;
        }
    }

    /*
    @TODO:
        inverse the gravity of the object
        @param: targetObject - the object that current magic interact with
    */
    private void inverseGravity(Rigidbody targetObject){

    }
}
