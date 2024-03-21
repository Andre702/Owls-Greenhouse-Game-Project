using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using System.Reflection;
using System;
using UnityEngine.SceneManagement;

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

    public void DigPlant(int index)
    {
        GameData.instance.DeletePlantData(index);
        // do something with dug plant here
    }

    public void PlantPlant(int index, PlantName name)
    {
        GameData.instance.SetPlantData(index, name, 0, 0);
    }

    public void GrowAll()
    {
        

        // Change hour in timer
        // Check for end of the day
        
    }

    public void GoForest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
