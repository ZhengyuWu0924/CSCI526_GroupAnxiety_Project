using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStar : MonoBehaviour
{
    public Sprite brightStar;
    public Sprite emptyStar;
    public Image[] starBar;

    void OnEnable()
    {
        starBar = GetComponentsInChildren<Image>();
    }


    public void SetStars(int stars)
    {
        switch (stars)
        {
            case 0:
                break;

            case 1:
                starBar[1].sprite = brightStar;
                break;

            case 2:
                starBar[0].sprite = brightStar;
                starBar[1].sprite = emptyStar;
                starBar[2].sprite = brightStar;
                break;
            case 3:
                starBar[0].sprite = brightStar;
                starBar[1].sprite = brightStar;
                starBar[2].sprite = brightStar;
                break;
        }
    }
}
