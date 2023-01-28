using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private bool isActive = true;
    private int mass = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(isActive);
    }

    /*
        get the current active status for the current stone object
        @return: true if active, false if disactive
    */

    public bool getActiveStatus(){
        return isActive;
    }
    
    /*
        set the active status for the current stone object
        or deactivate the current stone object
    */
    public void setActiveStatus(bool val){
        isActive = val;
    }

    /*
        get the current mass of the current stone object
        @return: current max value in int
    */
    public int getMassVal(){
        return mass;
    }
    
    /*
        set the mass value to the curren stone object
    */
    public void setMassVal(int givenMass){
        mass = givenMass;
    }

    /*
    @TODO:
        change force to current stone based on its mass
        if mass less than a specific value, add vertical force
        so it might float on the sky
    */
    private void addForceToStone(){

    }
}
