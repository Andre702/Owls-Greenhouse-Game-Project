using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Plant index frefers to it's growth state. Everything should be calculatable from there.
    public PlantData[] plantStates = new PlantData[6];

    // time, hour, water level needs to be added here probably


}

