using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public static GardenManager instance;

    private GameData gameData;

    private void Awake()
    {
        // found this solution online. Should ensure that the instance of GardenManager is always present.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        LoadGardenData();
    }

    // Data will be saved in PlayerPrefs. It has 1MB of storage avaliable.

    public void LoadGardenData()
    {
        string jsonData = PlayerPrefs.GetString("GameData");
        if (!string.IsNullOrEmpty(jsonData))
        {
            gameData = JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            gameData = new GameData();
        }
        
    }

    public void SaveGardenData()
    {
        string jsonData = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("GameData", jsonData);
        PlayerPrefs.Save();
    }

    public PlantData GetPlantData (int plantIndex)
    {
        return gameData.plantStates[plantIndex];
    }

    public void SetPlant (int plantIndex, PlantName name, int growthState)
    {
        gameData.plantStates[plantIndex] = new PlantData(name, growthState);
        SaveGardenData();
    }

    public void DigPlant(int index)
    {
        if (index >= 0 && index < gameData.plantStates.Length)
        {
            gameData.plantStates[index] = null;
        }
    }

    public void PrintAllPlantData()
    {
        string allPlantDataString = "All Plant Data [click to view]:\n";

        for (int i = 0; i < gameData.plantStates.Length; i++)
        {
            PlantData plantData = gameData.plantStates[i];

            if (plantData != null)
            {
                allPlantDataString += "Plant " + i + ": Name - " + plantData.plantName + ", Growth State - " + plantData.growthState + "\n";
            }
            else
            {
                allPlantDataString += "Plant " + i + ": Empty\n";
            }
        }

        Debug.Log(allPlantDataString);
    }

    public void NextHour()
    {
        for (int i =0; i < gameData.plantStates.Length; i++)
        {
            gameData.plantStates[i].Grow();
        }

        // Change hour in timer
        // Check for end of the day
        
    }

    
}
