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


    void Start()
    {
        restartScript = GameObject.FindObjectOfType(typeof(Restart)) as Restart;
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
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Tutorial()
    {
        tutorial.SetActive(true);
    }

    public void BackToGame()
    {
        tutorial.SetActive(false);
        Resume();
    }

}
