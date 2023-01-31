using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class larger : MonoBehaviour
{
    public Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {

    }
    //public void Resize(float amount, Vector3 direction)
    //{
    //    transform.position += direction * amount / 2; // Move the object in the direction of scaling, so that the corner on ther side stays in place
    //    transform.localScale += direction * amount; // Scale object in the specified direction
    //}

    // Update is called once per frame
    void Update()
    {
        transform.localScale += scaleChange;
        transform.position += scaleChange;
        //Resize(5f, new Vector3(0f, 0f, 1f);

    }
}
