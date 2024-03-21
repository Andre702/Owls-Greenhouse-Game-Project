using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBase
{
    [System.Serializable]
    public class GameData : MonoBehaviour
    {
        public static GameData instance { get; private set; }
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

        private Plant[] plantStates = new Plant[6];

        private void Start()
        {
            for (int i = 0; i < plantStates.Length; i++)
            {
                plantStates[i] = new Plant(PlantName.EMPTY, 0, 0);
            }
        }

        public Plant GetPlantData(int index)
        {
            if (index >= 0 && index < plantStates.Length)
            {
                return plantStates[index];
            }
            else
            {
                Debug.LogError("Invalid plant index.");
                return null;
            }
        }

        public void SetPlantData(int index, Plant data)
        {
            if (index >= 0 && index < plantStates.Length)
            {
                plantStates[index] = data;
            }
            else
            {
                Debug.LogError("Invalid plant index.");
            }
        }

        public void DeletePlantData(int index)
        {
            if (index >= 0 && index < plantStates.Length)
            {
                plantStates[index] = new Plant(PlantName.EMPTY, 0, 0);
            }
            else
            {
                Debug.LogError("Invalid plant index.");
            }
        }

        public Plant[] GetAllPlants()
        {
            return plantStates;
        }

        public void PrintAllPlantsData()
        {
            string allPlantDataString = "All Plant Data [click to view]:\n";
            for (int i = 0; i < plantStates.Length; i++)
            {
                allPlantDataString += "Plant " + i + plantStates[i].ToString() + '\n';
            }
            Debug.Log(allPlantDataString);
        }

        // time, hour, water level needs to be added here probably


    }
}


