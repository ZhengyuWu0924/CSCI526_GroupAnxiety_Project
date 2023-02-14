using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointSendForm : MonoBehaviour
{
    private SendToGoogle stg;
    // Start is called before the first frame update
    void Start()
    {
        stg = GameObject.FindObjectOfType(typeof(SendToGoogle)) as SendToGoogle;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Win")){
            print("hit the endpoint");
            stg.Send();
        }
    }
}
