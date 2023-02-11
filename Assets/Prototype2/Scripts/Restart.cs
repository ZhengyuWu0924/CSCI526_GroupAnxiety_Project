using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private GameManager gm;
    private int generationTimes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTheGame(){
        gm = FindObjectOfType<GameManager>();
        gm.resetInk();
        generationTimes = gm.getGenerateTimes();
        generationTimes += 1;
        gm.setRegenerateTimes(generationTimes);
        print("times = " + generationTimes);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
