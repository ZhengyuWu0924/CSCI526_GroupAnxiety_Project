using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject textGameObject;
    [SerializeField] private GameObject activatedBar;
    [SerializeField] private string text;
    public bool activated = false;
    private void Start()
    {
        if(text != null)
        {
            textGameObject.GetComponent<TextMeshPro>().text = text;
        }
    }

    /*private void OnMouseOver()
    {
        textGameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        textGameObject.SetActive(false);
    }
    */
    public void showText()
    {
        textGameObject.SetActive(true);
    }
    public void clostText()
    {
        textGameObject.SetActive(false);
    }
    public void activate()
    {
        activated = true;
        activatedBar.SetActive(true);
    }
}
