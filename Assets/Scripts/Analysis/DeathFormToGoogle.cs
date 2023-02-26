using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathFormToGoogle : MonoBehaviour
{
    // [SerializeField] private string URL;
    private static string URL = "https://docs.google.com/forms/u/4/d/e/1FAIpQLSe_shYbBT5NxwjFOYv7GHy2lI17-r_dXH71L6DJ2vhbDj98jw/formResponse";
    // private int _levelId;
    // private string _deathPosition;
    // Start is called before the first frame update


    private static IEnumerator Post(string deathPosition){
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        // form.AddField("entry.972509864", levelId);
        form.AddField("entry.1351992338", deathPosition);
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
    public void Send(Vector2 playerPosition){
        // Temp version
        // All data are temporary and fake
        // _inkUsage = GameManager.RemainInk;
        // _sceneRegenTimes = GameManager.SceneRegenerationTimes;

        // _deathPosition = playerPosition;
        StartCoroutine(Post(playerPosition.ToString()));
    }
}
