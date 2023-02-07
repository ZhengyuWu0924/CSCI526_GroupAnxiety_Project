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
    protected override void Awake()
    {
        base.Awake();
    }



    // Initialize game status
    void Start()
    {
        remainInk = 50;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateInk(float ink)
    {
        remainInk -= ink;
        GameObject.Find("RemainInk").GetComponent<TextMeshProUGUI>().SetText("Remain Ink: " + remainInk.ToString("0.0"));
    }

}
