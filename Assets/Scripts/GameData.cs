using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBase
{
    [System.Serializable]
    public class GameData : MonoBehaviour
    {
        public static GameData instance { get; private set; }

        public Sprite[] sunflowerSheet;
        public Sprite[] sprilliaSheet;
        public Sprite[] hartleafSheet;
        public Sprite[] deadPlant;

        private Dictionary<PlantName, Sprite[]> plantImageMap = new Dictionary<PlantName, Sprite[]>();
        private Plant[] plantStates = new Plant[7];
        private int hour;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            for (int i = 0; i < plantStates.Length; i++)
            {
                plantStates[i] = new Plant(PlantName.EMPTY, 0, 0, i);
            }

            plantImageMap.Add(PlantName.UNKNOWN, null);
            plantImageMap.Add(PlantName.EMPTY, null);
            plantImageMap.Add(PlantName.DEAD, deadPlant);
            plantImageMap.Add(PlantName.Sunflower, sunflowerSheet);
            plantImageMap.Add(PlantName.Sprillia, sprilliaSheet);
            plantImageMap.Add(PlantName.Hartleaf, hartleafSheet);

            hour = 1;
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
                plantStates[index] = new Plant(PlantName.EMPTY, 0, 0, index);
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

        public Sprite[] GetPlantSpriteByName(PlantName name)
        {
            Sprite[] sprite;
            if (plantImageMap.TryGetValue(name, out sprite))
            {
                return sprite;
            }
            else
            {
                Debug.LogWarning($"Sprite not found for plant: {name}");
                return null;
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

        public int GetHour()
        {
            return hour;
        }

        public void IncrementHour()
        {
            hour += 1;
        }

        // time, hour, water level needs to be added here probably


    }
}


