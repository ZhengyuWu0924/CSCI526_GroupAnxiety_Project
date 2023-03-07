using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject objectPrefab;
    [SerializeField]
    private float generationDelay;
    [SerializeField]
    private float destroyTime = 1;
    
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= generationDelay)
        {
            GameObject trap = Instantiate(objectPrefab, gameObject.transform.position, objectPrefab.transform.rotation);
            timer = 0f;
            StartCoroutine(TrapDestory(trap));
        }

    }

    IEnumerator TrapDestory(GameObject trap)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(trap);
    }
}
