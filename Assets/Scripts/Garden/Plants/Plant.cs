using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantName
{
    EMPTY,
    UNKNOWN,
    Sunflower,
    Sprillia,
    Hartleaf
}

public class Plant
{
    public PlantName plantName;
    public int growthState;
    public int health;
    public bool isHappy;

    public Plant(PlantName name, int state, int hp, bool happy = true)
    {
        plantName = name;
        growthState = state;
        health = hp;
        isHappy = happy;
    }
    public Plant() 
    {
        plantName = PlantName.UNKNOWN;
    }

    public virtual void GrowEffect(int growState)
    {

    }

    public override string ToString()
    {
        return $"(Name: {plantName}, Growth: {growthState}, HP: {health}, Happy: {isHappy})";
    }
}
