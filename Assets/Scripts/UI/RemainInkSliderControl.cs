using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainInkSliderControl : MonoBehaviour
{

    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    public void UpdateSlider(float remainInk)
    {
        if(remainInk > slider.maxValue)
        {
            slider.maxValue = remainInk;
        }
        slider.value = remainInk;
    }

    public void InitializedSlider(float remainInk)
    {
        slider.maxValue = remainInk;
    }

}
