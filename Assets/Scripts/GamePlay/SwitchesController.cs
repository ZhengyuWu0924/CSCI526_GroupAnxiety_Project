using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchesController : MonoBehaviour
{
    public List<GameObject> objectsToDisappear;
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

            foreach(GameObject obj in objectsToDisappear)
            {
                Destroy(obj);
            }
        }
    }

}
