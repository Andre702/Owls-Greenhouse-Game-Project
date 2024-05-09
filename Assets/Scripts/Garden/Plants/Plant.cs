using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlantName
{
    EMPTY,
    UNKNOWN,
    DEAD,
    Sunflower,
    Sprillia,
    Hartleaf
}

public enum PlantNeed
{
    Alone,
    NearOther,
}

public class Plant
{
    public PlantName plantName; // describes type of the Plant
    public int plantAge; // can be described as current hour - hourPlanted
    public int plantHealth;
    public int index; // describes position of the plant (pot in which it is planted)
    public bool isHappy; // describes if plant should recieve damage this turn
    public int stage; // describes sprite number which plant should be displayed as:
                      // * usually plants have 7 stages of life from -1 to 5, where:
                      // -1 means empty
                      // 0 means sappling or default
                      // 5 (or last stage) means fully grown

    //Standard needs:
    public bool needsWater = false;


    public Plant(PlantName name, int age, int _health, int _index, int _stage = -1, bool _ishappy = true)
    {
        plantName = name;
        plantAge = age;
        plantHealth = _health;
        index = _index;
        isHappy = _ishappy;
        stage = _stage;
    }
    public Plant(int _index) 
    {
        plantName = PlantName.UNKNOWN;
        index = _index;
    }

    public Plant()
    {
        plantName = PlantName.EMPTY;
    }

    public void Grow()
    {
        plantAge += 1;
        if (stage >= 0 && plantName != PlantName.DEAD)
        {
            if (isHappy == false)
            {
                plantHealth -= 1;
                //Debug.Log($"{plantName}, planted on pot nr. {index} lost health");
                GardenManager.instance.PlantHealthDownVisualEffect(index);
            }
            if (plantHealth <= 0)
            {
                KillPlant();
                return;
            }
            else if (stage < 5)
            {
                GrowEffectList();
            }
        }
    }
    // Increases plant's age. (uses GrowEffectList())

    public virtual void GrowEffectList()
    {
    }
    // Defines effects for increasing plant's age.
    // Every Plant defines it's own effects upon reaching different ages.

    public void CheckHappiness()
    {
        isHappy = CheckNeeds();
        UpdateHappinessIcon();
    }
    // Invokes CheckNeeds and assignes happiness state based on the value returned.
    // Updates Happiness Icon based on current happiness state.

    public virtual bool CheckNeeds()
    {
        return true;
    }
    // Checks every distinct need for this plant and returns true if all are satisfied, false if at least 1 is not

    public void KillPlant()
    {
        stage = 0;
        isHappy = true;
        plantName = PlantName.DEAD;
    }

    public bool AttemptToWater()
    {
        if (needsWater)
        {
            needsWater = false;
            CheckHappiness();
            return true;
        }

        return false;
    }

    public void UpdateVisuals()
    {
        if (GardenManager.instance != null)
        {
            GardenManager.instance.PlantVisualChange(this);
        }
    }

    public void UpdateHappinessIcon()
    {
        if (GardenManager.instance != null)
        {
            GardenManager.instance.PlantIconChange(this.index, this.isHappy);
        }
    }

    public string FormDialogue(int flag)
    {
        switch (flag)
        {
            case 0: // Greetings
                return Greeting();

            case 1: // How do You feel?
                return HowFeel();

            case 2: // What will You need?
                return Needs();

            case 3: // How old are You?
                return Age();

            default:
                return "Um... what?";
        }
           
    }

    protected virtual string Greeting()
    {
        return "";
    }

    protected virtual string HowFeel()
    {
        return "";
    }

    protected virtual string Needs()
    {
        return "";
    }

    protected virtual string Age()
    {
        return "";
    }

    public override string ToString()
    {
        return $"(Name: {plantName}, Age: {plantAge}, Stage: {stage}, HP: {plantHealth}, Happy: {isHappy})";
    }

    
}
