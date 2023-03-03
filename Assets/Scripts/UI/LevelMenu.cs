using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public int totalLevels = 0;
    private LevelButton[] levelButtons;

    void Start()
    {
        Refresh();
    }

    void OnEnable()
    {
        levelButtons = GetComponentsInChildren<LevelButton>();
    }

    public void Refresh()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetUp(i + 1);
        }
    }
}
