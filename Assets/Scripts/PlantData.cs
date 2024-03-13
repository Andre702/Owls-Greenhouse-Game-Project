using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantData
{
    public PlantName plantName;
    public float growthState;

    public PlantData(PlantName name, int state)
    {
        plantName = name;
        growthState = state;
    }

    public PlantName GetPlantName()
    {
        return plantName;
    }

    public float GetGrowthState()
    {
        return growthState;
    }

    public void Grow()
    {
        growthState += 1;
    }
}
