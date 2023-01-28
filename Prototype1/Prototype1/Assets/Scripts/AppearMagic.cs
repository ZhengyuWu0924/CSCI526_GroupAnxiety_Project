using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearMagic : MonoBehaviour
{
    private Rigidbody woodRb;
    private Rigidbody stoneRb;
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
        if(magicObject.CompareTag("MagicObject"))
        {
            magicObject.SetActive(false);
        }
    }

    /*
    @TODO:
        set the active status of the target object
        @param: targetObject - the object that current magic interact with
        @param: activateIt - boolean value, determine if make the object appear or not
    */
    private void makeAppear(Rigidbody targetObject, bool activateIt){

    }

}
