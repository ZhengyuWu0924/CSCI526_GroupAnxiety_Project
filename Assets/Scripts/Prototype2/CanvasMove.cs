using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMove : MonoBehaviour
{
    Vector3 mousePositionOffset;
    public GameObject LinesDrawer;
    private bool isMovable = false;
    private Vector3 GetMouseWorldPosition(){
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown(){
        if (isMovable)
        {
            // Disable drawing
            gameObject.layer = LayerMask.NameToLayer("CantDrawOver");
            // Get mouse position offset with respect to the canvas
            mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
        }
    }

    private void OnMouseDrag(){
        if (isMovable)
        {   
            // Make the canvas follow the mouse
            transform.position = GetMouseWorldPosition() + mousePositionOffset;
        }
    }

    private void OnMouseUp(){
        // Enable drawing
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void EnableMove()
    {
        isMovable = true;
    }

    public void DisableMove()
    {
        isMovable = false;
    }
}
