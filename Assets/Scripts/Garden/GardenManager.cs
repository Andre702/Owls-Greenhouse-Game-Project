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

    public DialogueManager dialogueManager;

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
        GameManager.OnSkipTime += UseClock;
    }

    private void Update()
    {
        if (cursor.type != 0)
        {
            cursorImage.FollowMouse();

            if (Input.GetMouseButtonDown(1))
            {
                CursorClear();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (cursor.type == 1)
                {
                    if (PlantAtemptRelocate())
                    {
                        CursorClear();
                    }
                }
                else if (cursor.type == 2)
                {

                }
                
            }
        }
        
    }

    public Transform GetPotOfIndex(int index)
    {
        foreach (var pot in pots)
        {
            if (pot.GetComponent<Pot>().potIndex == index)
            {
                return pot;
            }
        }
        Debug.LogWarning("NO POT OF THAT INDEX");
        return null;
    }
    // As A special pinvisible plant of index 4 was added this function helps to safetely get the right pot with the right index.
    public void PlantDigUp(int index)
    {
        if (GameData.instance.GetPlantData(index).plantName == PlantName.EMPTY)
        {
            return;
        }

        CursorSetPlant(GameData.instance.GetPlantData(index));

        GameData.instance.DeletePlantData(index);
        Transform targetPot = GetPotOfIndex(index);
        targetPot.GetComponent<Pot>().RemovePlant();
        targetPot.GetChild(1).GetComponent<PlantImage>().DisablePlant();

        GameManager.instance.PlantAllCheckHappiness();

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


        GetPotOfIndex(index).GetChild(1).GetComponent<PlantImage>().EnablePlant(newPlantSpriteSheet, true);

        GameManager.instance.PlantAllCheckHappiness();
    }
    // Updates Plant data on GameData by switch based on plantName (Uses SetPlantData).
    // Sets sprite sheet for a PlantImage (Uses EnablePlant of PlantImage on specified index position).

    public void PlantVisualChange(Plant plant)
    {
        PlantImage plantImageTarget = GetPotOfIndex(plant.index).GetChild(1).GetComponent<PlantImage>();
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

    public void PlantIconChange(int index, bool isHappy)
    {
        PlantImage plantImageTarget = GetPotOfIndex(index).GetChild(1).GetComponent<PlantImage>();
        plantImageTarget.UpdateCurrentSprite(-1, isHappy);
    }
    // Changes only an icon. Works faster than PlantVisualChange when it is not necessary
    public void PlantAllVisualUpdate()
    {
        foreach (var plant in GameData.instance.GetAllPlants())
        {
            if (plant.plantName != PlantName.EMPTY)
            {
                (Sprite[] spriteSheet, Sprite icon) plantGraphics = GameData.instance.GetPlantGraphicsByName(plant.plantName);

                GetPotOfIndex(plant.index).GetChild(1).GetComponent<PlantImage>().EnablePlant(plantGraphics.spriteSheet, plant.isHappy, plant.stage);
                if (plant.plantName == PlantName.EMPTY)
                {
                    continue;
                }
                GetPotOfIndex(plant.index).GetComponent<Pot>().PlantPlant(plantGraphics.icon);
            }
        }
    }
    // Mass PlantVisualChange used on scene change to Garden
    // Uses data from GameData and invokes PlantVisualChange(plant) on every plant index

    public bool PlantAttemptToWater(int index)
    {
        return GameData.instance.GetPlantData(index).AttemptToWater();
    }

    public bool PlantAtemptRelocate()
    {
        if (cursor.targetPotIndex < 0)
        {
            Debug.Log("Cursor not over pot");
            return false;
        }

        Transform clickedPot = GetPotOfIndex(cursor.targetPotIndex);

        if (clickedPot.GetComponent<Pot>().isEmpty)
        {
            cursor.plant.index = cursor.targetPotIndex;
            // Change plant Index to the index of the pot

            GameData.instance.SetPlantData(cursor.targetPotIndex, cursor.plant);
            (Sprite[] spriteSheet, Sprite icon) plantGraphics = GameData.instance.GetPlantGraphicsByName(cursor.plant.plantName);
            clickedPot.GetChild(1).GetComponent<PlantImage>().EnablePlant(plantGraphics.spriteSheet, cursor.plant.isHappy, cursor.plant.stage);
            clickedPot.GetComponent<Pot>().PlantPlant(plantGraphics.icon);

            GameManager.instance.PlantAllCheckHappiness();
            return true;
        } 

        // clicked Pot is not empty
        return false;
    }
    // Envoked by Pot class when clicked on with apropriate state of cursor (full and carrying a plant)
    // Uses SetPlantData with plant data stored in the cursor and then plants it in the hovered over pot.
    
    public void CursorClear()
    {
        cursor = (false, 0, null, -1);
        cursorImage.gameObject.SetActive(false);
    }
    // Resets cursor data

    public void CursorSetPlant(Plant plantData)
    {
        cursor = (true, 1, plantData, -1);
        Debug.Log(cursor);
        if (cursor.plant.plantName == PlantName.DEAD)
        {
            CursorClear();
        }
        else
        {
            cursorImage.Enable(cursor.type); ;
        }
    }

    public void CursorSetQuestion()
    {
        cursor = (false, 2, null, -1);
        cursorImage.Enable(2);
    }

    public void ExplainObject(string explanation)
    {
        CursorClear();
        dialogueManager.BeginDialogue(explanation);
    }

    public bool UseClock()
    {
        if (cursor.type == 2)
        {
            return false;
            // ask question about the clock
        }
        else
        {
            return true;
        }
    }

    public bool UseJar()
    {
        if (cursor.type == 2)
        {
            return false;
            // ask question about the jar
        }
        else
        {
            return true;
            // add water level [from GameData] to water bucket
        }
    }

    public void GoForest()
    {
        GameManager.instance.StartTime();
        Hud.instance.ButtonsInteractable(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
