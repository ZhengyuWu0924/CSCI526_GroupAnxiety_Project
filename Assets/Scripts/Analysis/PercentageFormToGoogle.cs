using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PercentageFormToGoogle : MonoBehaviour
{
    private static string URL = "https://docs.google.com/forms/u/4/d/e/1FAIpQLSeHskjdmxVbavPaSc72Wt8aTp8wPCbK_sIDfEpOqzQLlXHFmg/formResponse";

    private static IEnumerator Post(string platformUsed, string gravityUsed, string magnetUsed, string woodUsed,
                                    string rockUsed, string vanishUsed){
        // Create the form and enter responses
        WWWForm form = new WWWForm();

        
        form.AddField("entry.243063455", platformUsed);
        
        form.AddField("entry.2053696736", gravityUsed);
        
        form.AddField("entry.1440530263", magnetUsed);

        form.AddField("entry.1107256970", woodUsed);

        form.AddField("entry.1197228638", rockUsed);

        form.AddField("entry.1847848406", vanishUsed);



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
    public void Send(float platformUsage, float gravityUsage, float magnetUsage, float woodUsage,
                    float rockUsage, float vanishUsage){
        StartCoroutine(Post(platformUsage.ToString(), gravityUsage.ToString(), magnetUsage.ToString(), woodUsage.ToString(), rockUsage.ToString(), vanishUsage.ToString()));
    }
}
