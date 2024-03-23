using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GardenManager : MonoBehaviour
{
    public static GardenManager instance { get; private set; }

    public Transform[] pots;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        PlantAllVisualUpdate();
    }

    public void PlantDigUp(int index)
    {
        GameData.instance.DeletePlantData(index);
        pots[index].GetChild(1).GetComponent<PlantImage>().DisablePlant();
    }
    // Updates Plant data on GameData (Uses DeletePlantData).
    // Disables PlantImage (Uses EnablePlant of PlantImage on specified index position).


    public static void PlantWither(int index)
    {
        GameData.instance.GetPlantData(index).plantHealth -= 1;
    }
    // Not implemented yet. Usage of this will be very small only to keep the access structure.

    public void PlantPlant(int index, PlantName name)
    {
        Plant newPlant;
        Sprite[] newPlantSheet = null;

        Debug.Log(name);

        switch (name)
        {
            case PlantName.Sunflower:
                newPlant = new Sunflower(index);
                newPlantSheet = GameData.instance.GetPlantSpriteByName(name);
                break;

            case PlantName.Sprillia:
                newPlant = new Sprillia(index);
                newPlantSheet = GameData.instance.GetPlantSpriteByName(name);
                break;

            case PlantName.Hartleaf:
                newPlant = new Hartleaf(index);
                newPlantSheet = GameData.instance.GetPlantSpriteByName(name);
                break;

            default:
                newPlant = new Plant(index);
                newPlantSheet = GameData.instance.GetPlantSpriteByName(name);
                break;
        }

        GameData.instance.SetPlantData(index, newPlant);
        //GameData.instance.PrintAllPlantsData();
        pots[index].GetChild(1).GetComponent<PlantImage>().EnablePlant(newPlantSheet);
    }
    // Updates Plant data on GameData by switch based on plantName (Uses SetPlantData).
    // Sets sprite sheet for a PlantImage (Uses EnablePlant of PlantImage on specified index position).

    public void PlantVisualChange(int index, int stage)
    {
        if (stage < 0)
        {
            Debug.Log("Plant nr " + index + " - " + GameData.instance.GetPlantData(index).plantName + " has withered.");
            Sprite deadPlant = GameData.instance.GetPlantSpriteByName(PlantName.DEAD)[0];
            pots[index].GetChild(1).GetComponent<PlantImage>().SetSprite(deadPlant);
        }
        else
        {
            Debug.Log("Plant nr " + index + " - " + GameData.instance.GetPlantData(index).plantName + " has grown to new stage.");
            pots[index].GetChild(1).GetComponent<PlantImage>().UpdateSprite(stage);
        }
        
    }
    // Changes sprite of PlantImage which is already enabled in specified index (Uses either:
    // SetSprite - if the plant is dead or,
    // UpdateSprite - if the plant is alive)

    public void PlantAllVisualUpdate()
    {
        for (int i = 0; i < GameData.instance.GetAllPlants().Length; i++)
        {
            Plant plant = GameData.instance.GetPlantData(i);
            if (plant.stage < 0)
            {
                pots[i].GetChild(1).GetComponent<PlantImage>().EnablePlant(GameData.instance.GetPlantSpriteByName(plant.plantName), 0);
            }
            else
            {
                pots[i].GetChild(1).GetComponent<PlantImage>().EnablePlant(GameData.instance.GetPlantSpriteByName(plant.plantName), plant.stage);
            }
            
        }
    }

    public void NextHour()
    {
        GameData.instance.IncrementHour();
        PlantAllGrow();
    }

    public void PlantAllGrow(int growth = 1)
    {
        for (int i = 0; i < GameData.instance.GetAllPlants().Length; i++)
        {
            GameData.instance.GetPlantData(i).Grow();
        }
    }

    public void GoForest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
