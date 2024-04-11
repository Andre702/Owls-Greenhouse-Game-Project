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
    public (bool isFull, bool isQuestion, Plant plant, int target) cursor;
    // right now it is: isFull, isQuestion, Plant carying, index of the target hovered over , 
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
        cursor = (true, false, GameData.instance.GetPlantData(index), -1);
        if (cursor.plant.plantName != PlantName.DEAD)
        {
            cursorImage.GetComponent<GreenhouseCursor>().EnableCursor(0);
        }
        else
        {
            CursorClear();
        }
        GameData.instance.DeletePlantData(index);
        pots[index].GetComponent<Pot>().RemovePlant();
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

    public void PlantVisualChange(Plant plant)
    {
        PlantImage plantImageTarget = pots[plant.index].GetChild(1).GetComponent<PlantImage>();
        if (plant.stage >= 0)
        {
            if (plant.plantName == PlantName.DEAD)
            {
                Sprite deadPlant = GameData.instance.GetPlantGraphicsByName(PlantName.DEAD).spriteSheet[0];
                plantImageTarget.ChangeImageToDead(deadPlant);
            }
            else
            {
                plantImageTarget.UpdateCurrentSprite(plant.stage, plant.isHappy);
            }
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

            pots[i].GetChild(1).GetComponent<PlantImage>().EnablePlant(plantGraphics.spriteSheet, plant.isHappy, plant.stage);
            pots[i].GetComponent<Pot>().PlantPlant(plantGraphics.icon);
        }
    }

    public bool PlantAttemptToWater(int index)
    {
        return GameData.instance.GetPlantData(index).AttemptToWater ();
    }

    public bool PlantAtemptRelocate()
    {
        if (cursor.target < 0)
        {
            Debug.Log("Cursor not over pot");
            return false;
        }
        cursor.plant.index = cursor.target;

        Debug.Log(cursor.plant.ToString());
        GameData.instance.SetPlantData(cursor.target, cursor.plant);
        (Sprite[] spriteSheet, Sprite icon) plantGraphics = GameData.instance.GetPlantGraphicsByName(cursor.plant.plantName);
        pots[cursor.target].GetChild(1).GetComponent<PlantImage>().EnablePlant(plantGraphics.spriteSheet, cursor.plant.isHappy, cursor.plant.stage);
        pots[cursor.target].GetComponent<Pot>().PlantPlant(plantGraphics.icon);

        return true;

    }

    public void CursorClear()
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
