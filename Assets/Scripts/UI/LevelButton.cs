using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    private int level;
    public TMP_Text levelText;

    public GameObject starBar;
    private LevelStar levelStar;
    private Button button;


    void OnEnable()
    {
        button = GetComponent<Button>();
        levelStar = Instantiate(starBar, gameObject.transform).GetComponent<LevelStar>();
    }

    public void SetUp(int level)
    {
        this.level = level;
        levelText.text = level.ToString();
        SetStars(0);
    }

    void SetStars(int stars)
    {
        levelStar.SetStars(stars);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level " + level.ToString());
    }

    public void OnClick()
    {
        LoadLevel();
    }
}
