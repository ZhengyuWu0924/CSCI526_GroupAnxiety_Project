using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public TMP_Text levelText;

    void Start()
    {
        levelText.text = level.ToString();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level " + level.ToString());
    }
}
