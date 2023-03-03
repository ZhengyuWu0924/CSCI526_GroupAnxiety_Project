using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonImageEnabler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject textObject;

    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textObject.SetActive(false);
    }
}
