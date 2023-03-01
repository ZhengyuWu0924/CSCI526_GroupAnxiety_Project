using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrapSendForm : MonoBehaviour
{
    public GameManager gm;
    // private TrapFormToGoogle tftg;
    string collisionTrap;

    void Start(){
        gm = FindObjectOfType<GameManager>();
        // tftg = GameObject.FindObjectOfType(typeof(TrapFormToGoogle)) as TrapFormToGoogle;
    } 

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Trap")){
            collisionTrap = collision.gameObject.name;
            switch(collisionTrap){
                case "Trap0":
                    gm.trapsHitted[0] += 1;
                    break;
                case "Trap1":
                    gm.trapsHitted[1] += 1;
                    break;
                case "Trap2":
                    gm.trapsHitted[2] += 1;
                    break;
                default:
                    break;
            }
        }
    }
}
