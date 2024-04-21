using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;


public class GameManager : MonoBehaviour
{
    // This class is used for functions used in both Garden and Forest

    public static GameManager instance { get; private set; }
    // instance of GameManager needs to be accessible from every scene
    // as the plants need to be constantly growing throughout the game

    public float secondsInHour = 10;
    private bool timeIsFlowing = false;
    private float currentTime = 0f;

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

    public void NextHour()
    {
        GameData.instance.IncrementHour();
        PlantAllGrow();
        GameData.instance.PrintAllPlantsData();
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
        for (int i = 0; i < GameData.instance.GetAllPlants().Length; i++)
        {
            GameData.instance.GetPlantData(i).Grow();
        }
    }
}
