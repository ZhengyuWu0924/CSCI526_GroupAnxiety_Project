using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchesController : MonoBehaviour
{
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
    if (collision.gameObject.name == "RockPen(Clone)")
    {
        GameObject objectToDisappear = GameObject.FindGameObjectWithTag("Disappear");

        Destroy(objectToDisappear);
    }
}

}
