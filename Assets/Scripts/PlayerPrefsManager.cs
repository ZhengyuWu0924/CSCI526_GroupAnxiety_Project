using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPrefsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    

    public int getStarsCollectedOnLevel(int level){
        string starCollectedLevel = "StarCollected_Level" + level.ToString();
        // get stars collected at target level
        int starsCollected = PlayerPrefs.GetInt(starCollectedLevel);
        return starsCollected;
    }

    public void updateLevelStars(int level, int stars){
        string starCollectedLevel = "StarCollected_Level" + level.ToString();
        int alreadyGot = getStarsCollectedOnLevel(level);
        print("already got" + alreadyGot);

        // set upper limit for star collection to three
        if(alreadyGot < 3){
            PlayerPrefs.SetInt(starCollectedLevel, stars);
        }
        print("CurrentLevel is" + starCollectedLevel + " and stars are: " + stars);
    }

    /*
        Implemenmt the below function after discussion
    */
    private void OnApplicationQuit(){
        // print("delete playerpref");
        // PlayerPrefs.DeleteAll();
    }

}
