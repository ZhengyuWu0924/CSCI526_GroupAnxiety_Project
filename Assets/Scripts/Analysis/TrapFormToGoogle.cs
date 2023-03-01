using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrapFormToGoogle : MonoBehaviour
{
    private static string URL = "https://docs.google.com/forms/u/4/d/e/1FAIpQLSdgsfbf9FaYYC-WvRv6rIK70OHOsVeGagI3RK-9F2cT6YqdHA/formResponse";

    private static IEnumerator Post(int[] trapArr){
        // Create the form and enter responses
        WWWForm form = new WWWForm();

        // trap 0 hitted times
        form.AddField("entry.1464565645", trapArr[0].ToString());
        // trap 1 hitted times
        form.AddField("entry.1465169559", trapArr[1].ToString());
        // trap 2 hitted times
        form.AddField("entry.1239040308", trapArr[2].ToString());

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
    public void Send(int[] trapInfoArr){
        StartCoroutine(Post(trapInfoArr));
    }
}
