using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using UnityEngine.SceneManagement;

public class GardenManager : MonoBehaviour
{
    // This class is used for managing all functions and visuals of the Garden scene

    public static GardenManager instance { get; private set; }

    public Transform[] pots;
    // list of pots references: used for planting system, relocating and 

    public Transform gameCanvas;
    // canvas reference: used for moving dragable objects closer to the camera so they won't be obscured

    //public GardenCursor cursor;
    public (bool isFull, int type, Plant plant, int targetPotIndex) cursor;
    public CursorImage cursorImage;
    // cursor: a small container that player can move and use for storing single plant data and relocating it
    // It will also be used when player can get information on different objects in the Garden (possibly Forest to)


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

    private void Update()
    {
        if (cursor.isFull)
        {
            cursorImage.FollowMouse();

            if (Input.GetMouseButtonDown(1))
            {
                cursorImage.Disable();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (PlantAtemptRelocate())
                {
                    cursorImage.Disable();
                    CursorClear();
                }
            }
        }
        
    }

    public void PlantDigUp(int index)
    {
        if (GameData.instance.GetPlantData(index).plantName == PlantName.EMPTY)
        {
            return;
        }

        cursor = (true, cursor.type, GameData.instance.GetPlantData(index), -1);
        if (cursor.plant.plantName == PlantName.DEAD)
        {
            CursorClear();
        }
        else
        {
            cursorImage.Enable(cursor.type); ;
        }
        GameData.instance.DeletePlantData(index);
        pots[index].GetComponent<Pot>().RemovePlant();
        pots[index].GetChild(1).GetComponent<PlantImage>().DisablePlant();

    }
    // Updates Plant data on GameData (Uses DeletePlantData).
    // Disables PlantImage (Uses EnablePlant of PlantImage on specified index position).

    public void PlantHealthDownVisualEffect(int index)
    {
        //Do something when plants gets damage
    }
    // Not implemented yet. Usage of this may be very small.

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
    // Mass PlantVisualChange used on scene change to Garden
    // Uses data from GameData and invokes PlantVisualChange(plant) on every plant index

    public bool PlantAttemptToWater(int index)
    {
        return GameData.instance.GetPlantData(index).AttemptToWater ();
    }

    public bool PlantAtemptRelocate()
    {
        if (cursor.targetPotIndex < 0)
        {
            Debug.Log("Cursor not over pot");
            return false;
        }
        cursor.plant.index = cursor.targetPotIndex;

        Debug.Log(cursor.plant.ToString());
        GameData.instance.SetPlantData(cursor.targetPotIndex, cursor.plant);
        (Sprite[] spriteSheet, Sprite icon) plantGraphics = GameData.instance.GetPlantGraphicsByName(cursor.plant.plantName);
        pots[cursor.targetPotIndex].GetChild(1).GetComponent<PlantImage>().EnablePlant(plantGraphics.spriteSheet, cursor.plant.isHappy, cursor.plant.stage);
        pots[cursor.targetPotIndex].GetComponent<Pot>().PlantPlant(plantGraphics.icon);

        return true;

    }
    // Envoked by Pot class when clicked on with apropriate state of cursor (full and carrying a plant)
    // Uses SetPlantData with plant data stored in the cursor and then plants it in the hovered over pot.
    public void CursorClear()
    {
        cursor = (false, 0, null, -1);
    }
    // Resets cursor data
    public void GoForest()
    {
        GameManager.instance.StartTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
