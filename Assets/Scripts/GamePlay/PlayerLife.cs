using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    public int remainLife = 5;
    private Restart restartScript;
    private GameManager gm;


    

    // Start is called before the first frame update
    void Start()
    {
        restartScript = GameObject.FindObjectOfType(typeof(Restart)) as Restart;
        gm = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Boundary")){
            Die();
        }
    }

    private void Die(){
        restartScript.ResetTheGame();
    }
}
