using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMove : MonoBehaviour
{
    Vector3 mousePositionOffset;
    Vector3 originPosition;
    public GameObject LinesDrawer;
    private Vector3 GetMouseWorldPosition(){
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /*
        on mouse down, record the origin position of the object
    */
    private void OnMouseDown(){
        LinesDrawer.SetActive(false);
        originPosition = gameObject.transform.position;
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    /*
        when dragging the mouse, move the object with the mouse
    */

    private void OnMouseDrag(){
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    /*
        reset the position of the item with the recorded position
    */
    private void OnMouseUp(){
        LinesDrawer.SetActive(true);
        //transform.position = originPosition;
    }


}
