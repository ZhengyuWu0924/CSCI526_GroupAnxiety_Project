using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Use GameManager.Instance to access game manager
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public static float remainInk;

    public static float RemainInk{
        get { return remainInk; }
        set { remainInk = value; }
    }
    public static int sceneRegenerationTimes;

    public static int SceneRegenerationTimes{
        get { return sceneRegenerationTimes; }
        set { sceneRegenerationTimes = value; }
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
        
    }

    public void resetInk(){
        remainInk = 100;
    }

    public float getInk()
    {
        return remainInk;
    } 

}
