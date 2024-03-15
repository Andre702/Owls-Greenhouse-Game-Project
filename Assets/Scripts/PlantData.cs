using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantData
{
    public PlantName plantName;
    public int growthState;
    public int health;
    public bool isHappy;

    public PlantData(PlantName name, int state, int hp, bool happy)
    {
        plantName = name;
        growthState = state;
        health = hp;
        isHappy = happy;
    }

    public void Grow(int? growth = null)
    {
        if (growth == null)
        {
            growthState += 1;
        }
        else
        {
            growthState += growth.Value;
        }
    }

    public override string ToString()
    {
        return $"(Name: {plantName}, Growth: {growthState}, HP: {health}, Happy: {isHappy})";
    }
}
