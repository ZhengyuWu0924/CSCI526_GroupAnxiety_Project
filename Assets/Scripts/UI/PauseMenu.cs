using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenuPanel;
    [SerializeField] public GameObject win;
    [SerializeField] public GameObject lose;
    private Restart restartScript;
    private Texture2D defaultCursor;


    void Start()
    {
        restartScript = GameObject.FindObjectOfType(typeof(Restart)) as Restart;
        defaultCursor = Resources.Load<Texture2D>("Sprites/Cursors/DefaultCursor");
    }

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.SetCursor(defaultCursor, new Vector2(5, 5), CursorMode.Auto);
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.SetCursor(defaultCursor, new Vector2(5, 5), CursorMode.Auto);
    }

    public void MainMenu()
    {
        //restartScript.ResetTheGame();
        win.SetActive(false);
        lose.SetActive(false);
        Resume();
        Cursor.SetCursor(defaultCursor, new Vector2(5, 5), CursorMode.Auto);
        SceneManager.LoadScene("Main Menu");
    }
    

    public void SelectLevel()
    {
        Resume();
        win.SetActive(false);
        lose.SetActive(false);
        Cursor.SetCursor(defaultCursor, new Vector2(5, 5), CursorMode.Auto);
        SceneManager.LoadScene("LevelSelection");
    }

    public void NextLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int nextLevel = int.Parse(sceneName.Substring(6)) + 1;
        Cursor.SetCursor(defaultCursor, new Vector2(5, 5), CursorMode.Auto);
        SceneManager.LoadScene("Level " + nextLevel.ToString());
    }
}
