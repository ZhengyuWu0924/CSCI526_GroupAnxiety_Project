using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanquishableControl : MonoBehaviour
{
    public float destroyDelay = 1;
    private void Start()
    {
        Invoke("selfDestruction", destroyDelay);
    }

    private void selfDestruction()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TestVanquish"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
