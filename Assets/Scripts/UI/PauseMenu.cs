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
    private bool pause = false;


    void Start()
    {
        restartScript = GameObject.FindObjectOfType(typeof(Restart)) as Restart;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (!pause)
            {
                Pause();
            } else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void MainMenu()
    {
        //restartScript.ResetTheGame();
        win.SetActive(false);
        lose.SetActive(false);
        Resume();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("Main Menu");
    }
    

    public void SelectLevel()
    {
        Resume();
        win.SetActive(false);
        lose.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("LevelSelection");
    }

    public void NextLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        // Debug.Log("Current scene name: " + sceneName);
        int nextLevel = int.Parse(sceneName.Substring(6)) + 1;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("Level " + nextLevel.ToString());
    }
}
