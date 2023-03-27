using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonImageEnabler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject textObject;
    public GameObject imageObject;

    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //textObject.SetActive(true);
        imageObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //textObject.SetActive(false);
        imageObject.SetActive(false);
    }
}
