using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetButtonOnClick : MonoBehaviour
{
    private Button button;
    private DrawingTool drawingTool;
    private GameObject buttonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        drawingTool = GameObject.Find("DrawingTool").GetComponent<DrawingTool>();
        button = GetComponent<Button>();
        buttonPrefab = Resources.Load("Prefabs/" + gameObject.name.Replace("Button", "")) as GameObject;
        button.onClick.AddListener(() => drawingTool.SwitchTools(buttonPrefab));
    }
}