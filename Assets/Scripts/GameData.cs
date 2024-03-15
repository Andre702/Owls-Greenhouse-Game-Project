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

        private PlantData[] plantStates = new PlantData[6];

        public PlantData GetPlantData(int index)
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

        public void SetPlantData(int index, PlantName name, int growthState, int hp, bool isHappy = true)
        {
            if (index >= 0 && index < plantStates.Length)
            {
                plantStates[index] = new PlantData(name, growthState, hp, isHappy);
            }
            else
            {
                Debug.LogError("Invalid plant index.");
            }
        }

        public void PlantWither(int index)
        {
            if (index >= 0 && index < plantStates.Length)
            {
                plantStates[index].health += 1;
            }
            else
            {
                Debug.LogError("Invalid plant index.");
            }
        }

        public void AllPlantsGrow(int? growth = null)
        {
            for (int i = 0; i < plantStates.Length; i++)
            {
                plantStates[i].Grow(growth);
            }
        }

        public void DeletePlantData(int index)
        {
            if (index >= 0 && index < plantStates.Length)
            {
                plantStates[index] = null;
            }
            else
            {
                Debug.LogError("Invalid plant index.");
            }
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


