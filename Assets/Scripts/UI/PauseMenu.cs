using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenuPanel;
    [SerializeField] public GameObject tutorial;
    [SerializeField] public GameObject win;
    [SerializeField] public GameObject lose;
    private Restart restartScript;
    private bool pause;


    void Start()
    {
        restartScript = GameObject.FindObjectOfType(typeof(Restart)) as Restart;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        restartScript.ResetTheGame();
        win.SetActive(false);
        lose.SetActive(false);
        Resume();
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Tutorial()
    {
        tutorial.SetActive(true);
    }

    public void SelectLevel()
    {
        Resume();
        SceneManager.LoadScene("LevelSelection");
    }
}
