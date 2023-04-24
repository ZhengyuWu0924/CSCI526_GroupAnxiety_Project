using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public int totalLevels = 0;
    private LevelButton[] levelButtons;
    private PlayerPrefsManager playerPrefsManager;

    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        playerPrefsManager = GameObject.FindObjectOfType(typeof(PlayerPrefsManager)) as PlayerPrefsManager;
        Refresh();
    }

    void OnEnable()
    {
        levelButtons = GetComponentsInChildren<LevelButton>();
    }

    public void Refresh()
    {
        for (int i = 0; i < levelButtons.Length - 1; i++)
        {
            int starAmount = playerPrefsManager.getStarsCollectedOnLevel(i + 1);
            levelButtons[i].SetUp(i + 1, Mathf.Min(starAmount, 3));
            // levelButtons[i].SetUp(i + 1, 0);
        }
    }

    public void LoadPlayground()
    {
        SceneManager.LoadScene("Playground");
    }
}
