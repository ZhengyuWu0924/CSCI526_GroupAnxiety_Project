using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainInkSliderControl : MonoBehaviour
{

    private Slider slider;
    private bool ifflash;
    private GameObject fillColor;
    [SerializeField]private float flashAlpha = 0.4f;
    void Awake()
    {
        slider = GetComponent<Slider>();
        fillColor = gameObject.transform.Find("InkFillColor").gameObject;
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

    private void Update()
    {
        if(!ifflash && slider.value < slider.maxValue / 4)
        {
            ifflash = true;
            StartCoroutine(FlashWhenLowInk());
        }
        if(slider.value >= slider.maxValue / 4)
        {
            ifflash = false;
        }
    }

    IEnumerator FlashWhenLowInk()
    {
        while(ifflash)
        {
            StartCoroutine(FadeTo(flashAlpha, 0.25f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(FadeTo(1, 0.25f));
            yield return new WaitForSeconds(0.25f);
        }
    }
    IEnumerator FadeTo(float aValue, float aTime)
    {
        Color color = fillColor.GetComponent<Image>().color;
        float alpha = fillColor.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            color.a = Mathf.Lerp(alpha, aValue, t);
            fillColor.GetComponent<Image>().color = color;
            yield return null;
        }
    }
}
