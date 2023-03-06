using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// Use GameManager.Instance to access game manager
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject levelUI;

    public GameState state;
    public GameState State {
        get { return state; } 
        set { state = value; }
    }

    public static float remainInk;
    
    public static float RemainInk{
        get { return remainInk; }
        set { remainInk = value; }
    }

    

    public static float inkCostByTrap;
    public static float InkCostByTrap{
        get { return inkCostByTrap; }
        set { inkCostByTrap = value; }
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

    public static int currentLevelStars;
    public static int CurrentLevelStars{
        get { return currentLevelStars; }
        set { currentLevelStars = value;}
    }

    [HideInInspector] public int[] trapsHitted;
    public int this[int index]{
        get { return trapsHitted[index]; }
        set { trapsHitted[index] = value; }
    }
    public void UpdateTrapHitted(int index, int newValue){
        trapsHitted[index] = newValue;
    }

    public static float platformInkUsed;
    public static float PlatformInkUsed{
        get { return platformInkUsed; }
        set { platformInkUsed = value; }
    }

    public static float gravityInkUsed;
    public static float GravityInkUsed{
        get { return gravityInkUsed; }
        set { gravityInkUsed = value; }
    }

    public static float magnetInkUsed;
    public static float MagnetInkUsed{
        get { return magnetInkUsed; }
        set { magnetInkUsed = value; }
    }

    public static float woodInkUsed;
    public static float WoodInkUsed{
        get { return woodInkUsed; }
        set { woodInkUsed = value; }
    }

    public static float rockInkUsed;
    public static float RockInkUsed{
        get { return rockInkUsed; }
        set { rockInkUsed = value; }
    }

    public static float vanishInkUsed;
    public static float VanishInkUsed{
        get { return vanishInkUsed; }
        set { vanishInkUsed = value; }
    }


    // ****** File Refference Declarations ******
    SendToGoogle stg;
    DeathFormToGoogle dtg;
    TrapFormToGoogle tftg;
    PercentageFormToGoogle pftg;
    PlayerPrefsManager ppm;

    protected override void Awake()
    {
        base.Awake();
    }



    // Initialize game status
    void Start()
    {
        platformInkUsed = 0.0f;
        gravityInkUsed = 0.0f;
        magnetInkUsed = 0.0f;
        woodInkUsed = 0.0f;
        rockInkUsed = 0.0f;
        vanishInkUsed = 0.0f;
        currentLevelStars = 0;
        trapsHitted = new int[3];
        stg = GameObject.FindObjectOfType(typeof(SendToGoogle)) as SendToGoogle;
        dtg = GameObject.FindObjectOfType(typeof(DeathFormToGoogle)) as DeathFormToGoogle;
        tftg = GameObject.FindObjectOfType(typeof(TrapFormToGoogle)) as TrapFormToGoogle;
        pftg = GameObject.FindObjectOfType(typeof(PercentageFormToGoogle)) as PercentageFormToGoogle;
        ppm = FindObjectOfType<PlayerPrefsManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    // ****** State Handlers ******

    /*
        Update the game state of GameManager
        @newState: target state that wants to transfer to.
        @return: call this method in other scripts, and attach that script with 
        a specific object if wants to trigger it in game scene. related handle function
        will be called based on the state parsed in.
    */
    public void UpdateGameState(){
        // State = newState;
        switch(State){
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.Pause:
                break;
            case GameState.Playing:
                break;
            case GameState.MainMenu:
                break;
            case GameState.GameQuit:
                break;
            default:
                // throw new ArgumentOutOfRangeException(nameof(State), State, null);
                break;
        }
        State = GameState.Default;
    }

    /*
        Triggered by victory state
        When victory, send the form with data to google for data analysis
        TBD: Add function calls for printing victory infomation and button for next level
    */

    private void HandleVictory(){
        updateCurrentLevel();
        SendAtVictory();
        SendTrapInfoAtVictory();
        SendUsageAtVictory();
        passStarsToPrefs(currentLevel, currentLevelStars);

    }

    /*
        Triggered by lose state
        When player died, record its position and send it to google for data analysis
        TBD: Add function calls for printing the lose information and restart button
    */
    private void HandleLose(){
        SendAtLose();
    }


    /*
        Triggered when enter pause state
        When pause, print the pause menu with buttons
        TBD: Add wake up functions for the pause menu, also record corrent infos
    */
    private void HandlePause(){

    }

    /*
        Triggered when switch back to play scene
        TBD: Add dis-active functions for the pause menu, and wake up the
        objects in playing scene
    */
    private void HandlePlaying(){

    }

    /*
        Trigger when back to main menu
        TBD: Add wake up functions for the main menu scene, and also activate
        buttons on the scene
    */
    private void HandleMainMenu(){

    }

    private void HandleGameQuit(){

    }




    // ****** Helper Functions ******

    public void updateInk(float ink)
    {
        remainInk -= ink;
        GameObject.Find("RemainInkText").GetComponent<TextMeshProUGUI>().SetText("Remain Ink: " + remainInk.ToString("0.0"));
        GameObject.Find("RemaimInkSlider").GetComponent<RemainInkSliderControl>().UpdateSlider(remainInk);
    }

    public void setInk(float ink)
    {
        remainInk = ink;


        GameObject.Find("RemainInkText").GetComponent<TextMeshProUGUI>().SetText("Remain Ink: " + remainInk.ToString("0.0"));
        GameObject.Find("RemaimInkSlider").GetComponent<RemainInkSliderControl>().InitializedSlider(remainInk);
        GameObject.Find("RemaimInkSlider").GetComponent<RemainInkSliderControl>().UpdateSlider(remainInk);
    }

    public void resetInk(){
        remainInk = 100;
    }

    public void resetCurrentLevelStars(){
        currentLevelStars = 0;
    }

    public void updateCurrentLevelStars(int inValue){
        currentLevelStars = inValue;
    }

    public float getInk()
    {
        return remainInk;
    } 
    
    private void SendAtVictory(){
        stg.Send(remainInk, sceneRegenerationTimes, currentLevel, currentLevelStars, inkCostByTrap);
    }

    private void SendAtLose(){
        dtg.Send(player.transform.position);
    }

    private void SendTrapInfoAtVictory(){
        tftg.Send(trapsHitted);
    }

    private void SendUsageAtVictory(){
        pftg.Send(currentLevel, platformInkUsed, gravityInkUsed, magnetInkUsed, woodInkUsed, rockInkUsed, vanishInkUsed);
    }

    private void updateCurrentLevel(){
        // get the name of current level, and make switch based on level name
        string currentSceneName = SceneManager.GetActiveScene().name;

        /*
            @TODO: Add more cases in later development
        */
        switch(currentSceneName){
            case "Level 1":
                currentLevel = 1;
                break;
            case "Level 2":
                currentLevel = 2;
                break;
            case "Level 3":
                currentLevel = 3;
                break;
            default:
                break;
        }
        print(currentSceneName);

        /*
            Test version, set to level 1 only
            remove below code after detailed implementation
        */
        currentLevel = 1;

    }

    private void passStarsToPrefs(int curLevel, int curLevelStars){
        int gotStars = ppm.getStarsCollectedOnLevel(curLevel);
        ppm.updateLevelStars(curLevel, curLevelStars);
        gotStars = ppm.getStarsCollectedOnLevel(curLevel);
    }

    private void resetCurrentLevelStar(){
        currentLevelStars = 0;
    }



}


public enum GameState {
    Default = 0,
    Victory = 1,
    Lose = 2,
    Pause = 3,
    Playing = 4,
    MainMenu = 5,
    GameQuit = 6,
}