using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    public Sprite villagerSprite;
    public string villagerName;
    public int villagerAge;
    public string villagerTask;
    public int villagerFood;

    public enum States {
        Idle, 
        Campfire, 
        Farming, 
        Swimming,
    }

    public States state = States.Idle;

    public void ChangeState(States stateTo) {
        if(state == stateTo) 
            return;  
        state = stateTo;  
        ChangeActionMessage(state);
    }
     
    public bool IsState(States stateTo) {        
        if(state == stateTo)
            return true;
        return false;
    }

    public bool IsIdle {
        get {            
            return IsState(States.Idle);
        }
    }
    
    public bool IsCampfire {
        get {
            return IsState(States.Campfire);
        }
    }

    public bool IsFarming {
        get {
            return IsState(States.Farming);
        }
    }

    public bool IsSwimming {
        get {
            return IsState(States.Swimming);
        }
    }

    public void ChangeActionMessage(States state)
    {
        if (state == States.Idle)
            GlobalVariables.Variables.currentlySelectedVillager.GetComponent<Villager>().villagerTask = "Idle.";
        else if (state == States.Farming)         
            GlobalVariables.Variables.currentlySelectedVillager.GetComponent<Villager>().villagerTask = "Collecting food from the farm.";
        else if (state == States.Campfire)         
            GlobalVariables.Variables.currentlySelectedVillager.GetComponent<Villager>().villagerTask = "Gathering around a campfire for warmth.";
        else if (state == States.Swimming)         
            GlobalVariables.Variables.currentlySelectedVillager.GetComponent<Villager>().villagerTask = "Swimming.";
        
    }
}
