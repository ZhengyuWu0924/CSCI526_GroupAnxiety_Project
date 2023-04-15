using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDownEffect : MonoBehaviour
{
    [SerializeField] private float movingHeight = 0.6f;
    [SerializeField] private float movingTime = 0.8f;
    private float originalY;
    public bool ifMove;
    // Start is called before the first frame update
    void Start()
    {
        ifMove = true;
        originalY = transform.position.y;
        StartCoroutine(MoveUpAndDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MoveY(float yValue, float yTime)
    {
        float y = transform.position.y;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / yTime)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(y, yValue, t), transform.position.z);
            yield return null;
        }
    }
    IEnumerator MoveUpAndDown()
    {
        while (ifMove)
        {
            StartCoroutine(MoveY(originalY + movingHeight, movingTime));
            yield return new WaitForSeconds(movingTime);
            StartCoroutine(MoveY(originalY, movingTime));
            yield return new WaitForSeconds(movingTime);
        }
    }
}
