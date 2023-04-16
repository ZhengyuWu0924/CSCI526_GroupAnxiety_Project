using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusController : MonoBehaviour
{
    private GameObject player;
    public Image statusImage;
    public Slider countDownSlider;
    private bool ifCountDown = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        statusImage = gameObject.transform.Find("StatusImage").GetComponent<Image>();
        countDownSlider = gameObject.transform.Find("CountDownSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if (ifCountDown)
        {
            if (countDownSlider.value > 0)
            {
                countDownSlider.value -= Time.deltaTime;
            }
            if (countDownSlider.value <= 0)
            {
                DeactivateStatus();
            }
        }
    }

    public void ActivateCountDown(float countDownTime)
    {
        countDownSlider.maxValue = countDownTime;
        countDownSlider.value = countDownTime;
        ifCountDown = true;
    }

    public void ActivateStatus(Sprite image)
    {
        DeactivateStatus();
        statusImage.sprite = image;
        gameObject.SetActive(true);
    }

    public void DeactivateStatus()
    {
        countDownSlider.value = 0;
        ifCountDown = false;
        gameObject.SetActive(false);
    }
}
