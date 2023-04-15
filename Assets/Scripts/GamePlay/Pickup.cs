using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private ToolType toolType;
    [SerializeField]
    private GameObject toolPrefab;
    [SerializeField]
    private GameObject toolButton;

    private DrawingTool drawingTool;

    [SerializeField] private float movingHeight = 0.5f;
    [SerializeField] private float movingTime = 0.5f;
    private float originalY;
    private void Start()
    {
        drawingTool = GameObject.Find("DrawingTool").GetComponent<DrawingTool>();
        originalY = transform.position.y;
        StartCoroutine(MoveUpAndDown());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            drawingTool.PickUpTool(toolType, toolPrefab, toolButton);
        }
    }
    
    public void activeButton()
    {
        toolButton.SetActive(true);
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
        while (true)
        {
            StartCoroutine(MoveY(originalY + movingHeight, movingTime));
            yield return new WaitForSeconds(movingTime);
            StartCoroutine(MoveY(originalY, movingTime));
            yield return new WaitForSeconds(movingTime);
        }
    }
}
