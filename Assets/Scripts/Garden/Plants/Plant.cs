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

public class Plant
{
    public PlantName plantName; // describes type of the Plant
    public int plantAge; // can be described as current hour - hourPlanted
    public int plantHealth;
    public int index; // describes position of the plant (pot in which it is planted)
    public bool isHappy; // describes if plant should recieve damage this turn
    public int stage; // describes sprite number which plant should be displayed as:
                      // * usually plants have 7 stages of life from -1 to 5, where:
                      // -1 means dead
                      // 0 means sappling
                      // 5 (or last stage) means fully grown

    public Plant(PlantName name, int age, int _health, int _index, bool _ishappy = true, int _stage = 0)
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
        if (plantHealth <= 0) 
        {
            return;
        }

        plantAge += 1;
        GrowEffectList();
    }
    // Increases plant's age. (uses GrowEffectList())

    public virtual void GrowEffectList()
    {
    }
    // Defines effects for increasing plant's age.
    // Every Plant defines it's own effects upon reaching different ages.

    public void IncrementStage()
    {
        stage += 1;
        UpdateVisualStage();
    }

    public void KillPlant()
    {
        stage = -1;
        plantName = PlantName.DEAD;
        UpdateVisualStage();
    }

    public void UpdateVisualStage()
    {
        GardenManager.instance.PlantVisualChange(index, stage);
    }

    public override string ToString()
    {
        return $"(Name: {plantName}, Growth: {plantAge}, HP: {plantHealth}, Happy: {isHappy})";
    }
}
