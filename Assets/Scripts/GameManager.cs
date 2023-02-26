using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Use GameManager.Instance to access game manager
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public GameObject Player;

    public static float remainInk;
    public GameState State {get; private set;}
    public static float RemainInk{
        get { return remainInk; }
        set { remainInk = value; }
    }
    public static int sceneRegenerationTimes;

    public static int SceneRegenerationTimes{
        get { return sceneRegenerationTimes; }
        set { sceneRegenerationTimes = value; }
    }

    public static int currentLevel;
    public static int CurrentLevel{
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    

    protected override void Awake()
    {
        base.Awake();
    }



    // Initialize game status
    void Start()
    {
        remainInk = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateInk(float ink)
    {
        remainInk -= ink;
        GameObject.Find("RemainInkText").GetComponent<TextMeshProUGUI>().SetText("Remain Ink: " + remainInk.ToString("0.0"));
        GameObject.Find("RemaimInkSlider").GetComponent<RemainInkSliderControl>().UpdateSlider(remainInk);
    }

    public void resetInk(){
        remainInk = 100;
    }

    public float getInk()
    {
        return remainInk;
    } 

    public void UpdateGameState(GameState newState){
        State = newState;
        switch(State){
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            case GameState.Pause:
                break;
            case GameState.Playing:
                break;
            case GameState.MainMenu:
                break;
            default:
                // throw new ArgumentOutOfRangeException(nameof(State), State, null);
                break;
        }
    }

    private void HandleVictory(){
        // SendToGoogle.Send(remainInk, sceneRegenerationTimes);
    }

    private void HandleLose(){
        // DeathFormToGoogle.Send(Player.transform.position);
    }


}


public enum GameState {
    Victory = 0,
    Lose = 1,
    Pause = 2,
    Playing = 3,
    MainMenu = 4,
}