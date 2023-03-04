using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPrefsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void updateLevelStars(int level, int stars){
        print("enter update level stars");
        string starCollectedLevel = "StarCollected_Level" + level.ToString();
        PlayerPrefs.SetInt(starCollectedLevel, stars);
        print("CurrentLevel is" + starCollectedLevel + " and stars are: " + stars);
    }

    public int getStarsCollectedOnLevel(int level){
        string starCollectedLevel = "StarCollected_Level" + level.ToString();

        // get stars collected at target level, if no stars, return 0 as default
        int starsCollected = PlayerPrefs.GetInt(starCollectedLevel, 0);
        return starsCollected;
    }

    private void OnApplicationQuit(){
        PlayerPrefs.DeleteAll();
    }

}
