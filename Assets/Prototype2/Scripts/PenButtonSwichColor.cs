using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenButtonSwichColor : MonoBehaviour
{
    public GameObject newButton;
    //public GameObject linesDrawer;
    private bool isActivated = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeColor()
    {
        //isActivated = linesDrawer.GetComponent<LinesDrawer>().canDraw;
        if (!isActivated)
        {
            isActivated = true;
            newButton.GetComponent<Image>().color = Color.green;
            
        }else
        {
            isActivated = false;
            newButton.GetComponent<Image>().color = Color.white;
        }
        
    }
}
