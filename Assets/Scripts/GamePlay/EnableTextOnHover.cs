using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class EnableTextOnHover : MonoBehaviour
{
    public GameObject textObject;

    private void OnMouseOver()
    {
        Debug.Log("mose over");
        textObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        textObject.SetActive(false);
    }
}
