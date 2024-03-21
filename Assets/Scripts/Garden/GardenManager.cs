using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using System.Reflection;
using System;
using UnityEngine.Events;

public class GardenManager : MonoBehaviour
{
    public static GardenManager instance { get; private set; }  

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    public void PlantDigUp(int index)
    {
        GameData.instance.DeletePlantData(index);
    }

    public static void PlantWither(int index)
    {
        GameData.instance.GetPlantData(index).health -= 1;
    }

    public void PlantAllGrow(int growth = 1)
    {
        for (int i = 0; i < GameData.instance.GetAllPlants().Length; i++)
        {
            GameData.instance.GetPlantData(i).growthState += 1;
        }
    }

    public void PlantPlant(int index, PlantName name)
    {
        Plant newPlant;
        switch (name)
        {
            case PlantName.Sunflower:
                newPlant = new Sunflower();
                break;

            case PlantName.Sprillia:
                newPlant = new Sprillia();
                break;

            case PlantName.Hartleaf:
                newPlant = new Hartleaf();
                break;

            default:
                newPlant = new Plant();
                break;
        }

        GameData.instance.SetPlantData(index, newPlant);
        GameData.instance.PrintAllPlantsData();
    }

    public void GrowAll()
    {
        

        // Change hour in timer
        // Check for end of the day
        
    }

    
}
