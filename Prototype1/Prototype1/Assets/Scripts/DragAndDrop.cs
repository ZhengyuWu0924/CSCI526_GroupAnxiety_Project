using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePositionOffset;
    
    private Vector3 GetMouseWorldPosition(){
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown(){
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag(){
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    /*
    @TODO:
        reset the position of the item
    */
    private void OnMouseUp(){

    }


}
