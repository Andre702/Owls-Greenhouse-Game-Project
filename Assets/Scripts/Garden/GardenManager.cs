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
    public (bool, bool, Plant, int) cursor;
    // right now it is: isFull, isQuestion, Plant carying, index of the plant
    public Transform cursorImage;
    public Transform waterContainer;
    
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
        cursor = (true, false, GameData.instance.GetPlantData(index), index);
        cursorImage.GetComponent<GreenhouseCursor>().EnableCursor(0);
        GameData.instance.DeletePlantData(index);
        pots[index].GetChild(1).GetComponent<PlantImage>().DisablePlant();
    }
    // Updates Plant data on GameData (Uses DeletePlantData).
    // Disables PlantImage (Uses EnablePlant of PlantImage on specified index position).


    public void PlantHealthDown(int index)
    {
        //Do something when plants gets damage
    }
    // Not implemented yet. Usage of this will be very small only to keep the access structure.

    public void PlantPlant(int index, PlantName name)
    {
        Plant newPlant;
        Sprite[] newPlantSpriteSheet = GameData.instance.GetPlantGraphicsByName(name).spriteSheet;
        // (Plant Sprite Sheet, Plant Icon)
        Debug.Log(name);

        switch (name)
        {
            case PlantName.Sunflower:
                newPlant = new Sunflower(index);
                break;

            case PlantName.Sprillia:
                newPlant = new Sprillia(index);
                break;

            case PlantName.Hartleaf:
                newPlant = new Hartleaf(index);
                break;

            default:
                newPlant = new Plant(index);
                break;
        }

        GameData.instance.SetPlantData(index, newPlant);
        //GameData.instance.PrintAllPlantsData();
        pots[index].GetChild(1).GetComponent<PlantImage>().EnablePlant(newPlantSpriteSheet, true);
    }
    // Updates Plant data on GameData by switch based on plantName (Uses SetPlantData).
    // Sets sprite sheet for a PlantImage (Uses EnablePlant of PlantImage on specified index position).

    public void PlantVisualChange(int index, int stage, bool isHappy)
    {
        PlantImage plantTarget = pots[index].GetChild(1).GetComponent<PlantImage>();
        if (stage < 0)
        {
            //Debug.Log("Plant nr " + index + " - " + GameData.instance.GetPlantData(index).plantName + " has withered.");
            Sprite deadPlant = GameData.instance.GetPlantGraphicsByName(PlantName.DEAD).spriteSheet[0];
            plantTarget.ChangeImageToDead(deadPlant);
        }
        else
        {
            //Debug.Log("Plant nr " + index + " - " + GameData.instance.GetPlantData(index).plantName + " has grown to new stage.");
            plantTarget.UpdateCurrentSprite(stage, isHappy);
        }
        
    }
    // Changes sprite of PlantImage which is already enabled in specified index (Uses either:
    // ChangeImageToDead - if the plant is dead or,
    // UpdateCurrentSprite - if the plant is alive)

    public void PlantAllVisualUpdate()
    {
        for (int i = 0; i < GameData.instance.GetAllPlants().Length; i++)
        {
            Plant plant = GameData.instance.GetPlantData(i);
            (Sprite[] spriteSheet, Sprite icon) plantGraphics = GameData.instance.GetPlantGraphicsByName(plant.plantName);

            if (plant.stage < 0)
            {
                pots[i].GetChild(1).GetComponent<PlantImage>().EnablePlant(plantGraphics.spriteSheet, true, 0);
                pots[i].GetComponent<Pot>().PlantPlant(plantGraphics.icon);
            }
            else
            {
                pots[i].GetChild(1).GetComponent<PlantImage>().EnablePlant(plantGraphics.spriteSheet, plant.isHappy, plant.stage);
                pots[i].GetComponent<Pot>().PlantPlant(plantGraphics.icon);
            }
            
        }
    }

    public bool PlantAttemptToWater(int index)
    {
        return GameData.instance.GetPlantData(index).AttemptToWater();
    }
    public void ClearCursor()
    {
        cursor = (false, false, null, -1);
    }

    public void NextHour()
    {
        GameData.instance.IncrementHour();
        PlantAllGrow();
        GameData.instance.PrintAllPlantsData();
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
        GameData.instance.StartTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
