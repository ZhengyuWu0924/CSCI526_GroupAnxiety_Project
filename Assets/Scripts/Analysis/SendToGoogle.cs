using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    // URL is https://docs.google.com/forms/u/4/d/e/1FAIpQLSdOFLieYVjan5B-iP79DfZ8M6S5c_thnbpF3sSTJM9rvcxOTg/formResponse
    // [SerializeField] private string URL;
    private string URL = "https://docs.google.com/forms/u/4/d/e/1FAIpQLSdOFLieYVjan5B-iP79DfZ8M6S5c_thnbpF3sSTJM9rvcxOTg/formResponse";
    private long _sessionID;
    private float _inkUsage;
    private int _sceneRegenTimes;
    private int _checkpoints;
    private int _rewardCollected;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
        Generate a unique session ID for current game play
    */
    private void Awake(){
        _sessionID = DateTime.Now.Ticks;
    
        // At test version, call send method when game awake
        // change the calling later
        // Send();
    }

    private IEnumerator Post(string sessionID, string inkUsage, string sceneRegenTimes){
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.366340186", sessionID);
        form.AddField("entry.161804138", inkUsage);
        form.AddField("entry.515608870", sceneRegenTimes);
        // form.AddField("entry.1197245544", checkPoints);
        // form.AddField("entry.772491702", rewardCollected);

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
    public void Send(){
        // Temp version
        // All data are temporary and fake
        _inkUsage = GameManager.RemainInk;
        _sceneRegenTimes = GameManager.SceneRegenerationTimes;

        StartCoroutine(Post(_sessionID.ToString(), _inkUsage.ToString(), _sceneRegenTimes.ToString()));
    }
}
