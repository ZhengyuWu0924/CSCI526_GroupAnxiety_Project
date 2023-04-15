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

    private void Start()
    {
        drawingTool = GameObject.Find("DrawingTool").GetComponent<DrawingTool>();
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
}
