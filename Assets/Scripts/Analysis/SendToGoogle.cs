using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    // URL is https://docs.google.com/forms/u/4/d/e/1FAIpQLSdOFLieYVjan5B-iP79DfZ8M6S5c_thnbpF3sSTJM9rvcxOTg/formResponse
    // [SerializeField] private string URL;
    private static string URL = "https://docs.google.com/forms/u/4/d/e/1FAIpQLSdOFLieYVjan5B-iP79DfZ8M6S5c_thnbpF3sSTJM9rvcxOTg/formResponse";
    // private static long _sessionID;
    // private float _inkUsage;
    // private static int _sceneRegenTimes;
    // private static int _checkpoints;
    // private static int _rewardCollected;
    // private static float _timeUsedBetweenCheckpoints;



    private static IEnumerator Post(string sessionID, string inkUsage, string sceneRegenTimes, string levelId, string rewards, string inkByTrap){
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.366340186", sessionID);
        form.AddField("entry.671983043", levelId);
        form.AddField("entry.161804138", inkUsage);
        form.AddField("entry.210016202", inkByTrap);
        form.AddField("entry.515608870", sceneRegenTimes);
        form.AddField("entry.772491702", rewards);

        // Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)){
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success){
                Debug.Log(www.error);
            } else {
                Debug.Log("Form Upload Completed!");
            }
        }

    }
    

    /*
        Send data to Google Form
        Call this method only when decide to send all data to Google
    */
    public void Send(float inkUsed, int regenTimes, int curLevelId, int curRewards, float inkCostByTrap){
        // long _sessionID = DateTime.Now.Ticks;
        // Temp version
        // All data are temporary and fake
        // _inkUsage = inkUsed;
        // _sceneRegenTimes = regenTimes;

        StartCoroutine(Post(DateTime.Now.Ticks.ToString(), inkUsed.ToString(), regenTimes.ToString(), curLevelId.ToString(), curRewards.ToString(), inkCostByTrap.ToString()));
    }
}
