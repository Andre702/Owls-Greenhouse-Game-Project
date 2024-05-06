using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;


public class GameManager : MonoBehaviour
{
    // This class is used for functions used in both Garden and Forest

    public delegate bool SkipTimeHandler();
    public static event SkipTimeHandler OnSkipTime;

    public static GameManager instance { get; private set; }
    // instance of GameManager needs to be accessible from every scene
    // as the plants need to be constantly growing throughout the game

    public float secondsInHour = 10;
    private bool timeIsFlowing = false;
    private float currentTime = 0f;

    public bool isSceneGarden = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (timeIsFlowing)
        {
            currentTime += Time.deltaTime;
        }

        if (currentTime >= secondsInHour)
        {
            currentTime = 0f;
            NextHour();

            Debug.Log("Hour passes");
        }
    }

    public bool PlantNeedsCheck(int plantIndex, PlantNeed need)
    {
        
        switch (need)
        {
            case PlantNeed.Alone:
                if (GameData.instance.GetPlantData(plantIndex - 1).plantName == PlantName.EMPTY &&
                    GameData.instance.GetPlantData(plantIndex + 1).plantName == PlantName.EMPTY
                    )
                {
                    return true;
                }
                return false;
            default:
                Debug.LogWarning("Need not implemented yet!");
                return false;

        }
    }
    public void InvokeSkipTime()
    {
        if (OnSkipTime != null)
        {
            if (OnSkipTime())
            {
                NextHour();
            }
            else
            {
                Debug.Log("Time Skip prevented by event from Garden");
            }
        }
        else
        {
            NextHour();
        }
    }

    public void NextHour()
    {
        GameData.instance.IncrementHour();
        PlantAllGrow();
        PlantAllCheckHappiness();
        Hud.instance.UpdateHourDisplay();
    }

    public void StartTime()
    {
        timeIsFlowing = true;
    }

    public void StopTime()
    {
        timeIsFlowing = false;
        currentTime = 0f;
    }

    public void PlantAllGrow(int growth = 1)
    {
        foreach (var plantData in GameData.instance.GetAllPlants())
        {
            if (plantData.plantName != PlantName.EMPTY)
            {
                plantData.Grow();
                plantData.UpdateVisuals();
            }
        }
    }

    public void PlantAllCheckHappiness(int growth = 1)
    {
        foreach (var plantData in GameData.instance.GetAllPlants())
        {
            if (plantData.plantName != PlantName.EMPTY)
            {
                plantData.CheckHappiness();
            }
        }
    }
}
