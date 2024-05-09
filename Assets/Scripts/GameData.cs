using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBase
{
    [System.Serializable]
    public class GameData : MonoBehaviour
    {
        // This class is used for storage and quick recovery of all primary and persisting game data
        
        public static GameData instance { get; private set; }
        // in order to access this instance a class must be using DataBase namespace ^

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

            plantImageMap.Add(PlantName.UNKNOWN, (null, null));
            plantImageMap.Add(PlantName.EMPTY, (null, null));
            plantImageMap.Add(PlantName.DEAD, (deadPlant, plantIcons[3]));
            plantImageMap.Add(PlantName.Sunflower, (sunflowerSheet, plantIcons[0]));
            plantImageMap.Add(PlantName.Sprillia, (sprilliaSheet, plantIcons[1]));
            plantImageMap.Add(PlantName.Hartleaf, (hartleafSheet, plantIcons[2]));

            hour = 1;
        }

        #region Plant related ============================================================================================

        public Sprite[] sunflowerSheet;
        public Sprite[] sprilliaSheet;
        public Sprite[] hartleafSheet;
        public Sprite[] deadPlant;
        public Sprite[] plantIcons;

        private Dictionary<PlantName, (Sprite[], Sprite)> plantImageMap = new Dictionary<PlantName, (Sprite[], Sprite)>();
        private Plant[] plantStates = new Plant[8];

        public Plant GetPlantData(int index)
        {
            if (index >= 0 && index < plantStates.Length)
            {
                return plantStates[index];
            }
            else
            {
                //Debug.LogError("Invalid plant index.");
                return new Plant();
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

        public (Sprite[] spriteSheet, Sprite icon) GetPlantGraphicsByName(PlantName name)
        {
            (Sprite[] spriteSheet, Sprite icon) sprite;
            if (plantImageMap.TryGetValue(name, out sprite))
            {
                return sprite;
            }
            else
            {
                Debug.LogWarning($"Sprite not found for plant: {name}");
                return (null, null);
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
        #endregion 


        #region Time related ============================================================================================    

        public int hour;
        public string clockDescription = "This is my special Magical Watch. It will skip exactly 1 hour when used so be precise with it.\n" +
            "The watch is powered by the greenhouse and it will not work outside of it.";

        public int GetHour()
        {
            return hour;
        }
       
        public void IncrementHour()
        {
            hour += 1;
        }

        #endregion

        #region Water ===========================================================================================
        float playerWater = 0;
        float waterChanges = 100;

        public string jarDescription = "This is a Water Jug. You can use it to gather water from the nearby lake and then pour it into the barrel.\n" +
            "It will not break but if you trip and fall it will spill some water so be careful in the forest.";

        public float changePlayerWater(float water) 
        {
            playerWater += water;
            if (playerWater > 100) playerWater = 100;
            if (playerWater < 0) playerWater = 0;
            return playerWater;
        }

        public float getPlayerWater()
        {
            return playerWater;
        }

        public void resetPlayerWater()
        {
            playerWater = 0;
        }

        public void saveWaterLevel(float water)
        {
            waterChanges = water;
        }

        public float getWaterSaved()
        {
            return waterChanges;
        }
        #endregion
    }
}


