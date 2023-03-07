using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrapTrigger : MonoBehaviour
{
    private GameManager gm;
    // private TrapFormToGoogle tftg;
    private string collisionTrap;
    [SerializeField]
    private float invincibleTime = 3;
    [SerializeField]
    private float invincibleAlpha = 0.4f;

    private bool isInvincible = false;
    void Start(){
        gm = FindObjectOfType<GameManager>();
        // tftg = GameObject.FindObjectOfType(typeof(TrapFormToGoogle)) as TrapFormToGoogle;
    } 

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Trap") && !isInvincible){
            StartInvincible();
            float beforeUpdate = GetComponent<PlayerController>().remainInk;
            gm.updateInk(GetComponent<PlayerController>().remainInk * 0.333f);
            GameManager.inkCostByTrap += (beforeUpdate - GetComponent<PlayerController>().remainInk);
            collisionTrap = collision.gameObject.name;
            switch(collisionTrap){
                case "Trap0":
                    gm.trapsHitted[0] += 1;
                    break;
                case "Trap1":
                    gm.trapsHitted[1] += 1;
                    break;
                case "CircleTrap(Clone)":
                    gm.trapsHitted[2] += 1;
                    break;
                default:
                    break;
            }
            /*
                Character dies if remain ink less than 0 after hitted a trap
            */
            if(GetComponent<PlayerController>().remainInk <= 0){
                // update game state to lose state
                // and trigger 
                gm.state = GameState.Lose;
                gm.UpdateGameState();
            }
        }
    }
    private void StartInvincible()
    {
        isInvincible = true;
        StartCoroutine(InvinsibleEffect());
    }
    IEnumerator InvinsibleEffect()
    {
        for(int i=0; i<invincibleTime; i++)
        {
            StartCoroutine(FadeTo(invincibleAlpha, 0.25f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(FadeTo(1, 0.25f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(FadeTo(invincibleAlpha, 0.25f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(FadeTo(1, 0.25f));
            yield return new WaitForSeconds(0.25f);
        }
        isInvincible = false;
    }
    IEnumerator FadeTo(float aValue, float aTime)
    {
        Color color = GetComponent<SpriteRenderer>().color;
        float alpha = GetComponent<SpriteRenderer>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            color.a = Mathf.Lerp(alpha, aValue, t);
            GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
    }
}
