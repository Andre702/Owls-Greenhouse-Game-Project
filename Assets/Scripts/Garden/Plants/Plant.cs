using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Grow()
    {
        if (stage >= 0)
        {

            if (isHappy == false)
            {
                plantHealth -= 1;
                GardenManager.instance.PlantHealthDownVisualEffect(index);
            }
            if (plantHealth <= 0)
            {
                KillPlant();
            }
            else
            {
                plantAge += 1;
                GrowEffectList();
            }
            UpdateVisuals();
        }
    }
    // Increases plant's age. (uses GrowEffectList())

    public virtual void GrowEffectList()
    {
    }
    // Defines effects for increasing plant's age.
    // Every Plant defines it's own effects upon reaching different ages.

    public virtual bool CheckHappiness()
    {
        return true;
    }

    public void KillPlant()
    {
        stage = 0;
        isHappy = true;
        plantName = PlantName.DEAD;
    }

    public bool AttemptToWater()
    {
        if (!isHappy)
        {
            isHappy = true;
            UpdateVisuals();
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

    public override string ToString()
    {
        return $"(Name: {plantName}, Age: {plantAge}, Stage: {stage}, HP: {plantHealth}, Happy: {isHappy})";
    }
}
