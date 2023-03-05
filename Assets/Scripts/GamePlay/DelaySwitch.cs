using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySwitch : MonoBehaviour
{
    public List<GameObject> objectsToDisappear;
    [SerializeField]
    private int delayTime = 2;
    private bool isTriggerd = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTriggerd)
        {
            isTriggerd = true;
            foreach (GameObject obj in objectsToDisappear)
            {
                StartCoroutine(Disappear(obj));
            }
        }
    }
    IEnumerator Disappear(GameObject obj)
    {
        for (int i = 0; i < delayTime; i++)
        {
            StartCoroutine(FadeTo(obj, 0.6f, 0.5f));
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FadeTo(obj, 1, 0.5f));
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(obj);
    }
    IEnumerator FadeTo(GameObject obj, float aValue, float aTime)
    {
        Color color = obj.GetComponent<SpriteRenderer>().color;
        float alpha = obj.GetComponent<SpriteRenderer>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            color.a = Mathf.Lerp(alpha, aValue, t);
            obj.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
    }
}
