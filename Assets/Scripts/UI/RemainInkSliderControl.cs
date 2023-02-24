using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainInkSliderControl : MonoBehaviour
{

    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    public void UpdateSlider(float remainInk)
    {
        slider.value = remainInk;
    }
}
