using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitch : MonoBehaviour
{
    private bool isOpen = true;
    public Text drawingBoardSwitchText;
    public void Switch()
    {
        if (isOpen)
        {
            isOpen = false;
            drawingBoardSwitchText.text = "Open Drawing Board";
            gameObject.SetActive(isOpen);
        }
        else
        {
            isOpen = true;
            drawingBoardSwitchText.text = "Close Drawing Board";
            gameObject.SetActive(isOpen);
        }
    }
}
