using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataBase
{
    [System.Serializable]
    public class GameData : MonoBehaviour
    {
        // This class is used for storage and recovery of all primary and persisting game data
        
        public static GameData instance { get; private set; }
        // in order to access this instance a class must be using DataBase namespace ^

        public bool gameStartedFirstTime = false;
        public bool gameFinished = true;
        public int score = 0;

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
            plantImageMap.Add(PlantName.Heartleaf, (hartleafSheet, plantIcons[2]));

            hour = 1;
            barrelWater = 40;
            playerWater = 0;
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

        public int[] CountAdultPlants()
        {
            int[] plantCountarray = { 0, 0, 0 };
            foreach (Plant p in GetAllPlants())
            {
                if (p.plantName != PlantName.EMPTY && p.stage >= 5)
                {
                    switch (p.plantName)
                    {
                        case PlantName.Sunflower:
                            plantCountarray[0]++;
                            break;

                        case PlantName.Sprillia:
                            plantCountarray[1]++;
                            break;

                        case PlantName.Heartleaf:
                            plantCountarray[2]++;
                            break;

                        default:
                            Debug.LogWarning($"Plant named {p.plantName} not included in wictory condition count!");
                            break;

                    }
                }
            }

            return plantCountarray;
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
        public string clockDescription = "This is my special Magical Watch. It will skip exactly 1 hour when used so be precise with it.|" +
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

        #region Water related ===========================================================================================
        
        private float playerWater;
        private float barrelWater;

        public string jarDescription = "This is a Water Jar. You can use it to gather water from the nearby lake and then pour it into the barrel.|" +
            "It will not break but if You trip and fall it will spill some water so be careful in the forest.";

        public float AddPlayerWater(float water) 
        {
            playerWater += water;
            if (playerWater > 100) playerWater = 100;
            if (playerWater < 0) playerWater = 0;
            Hud.instance.UpdateWaterDisplay();
            return playerWater;
        }

        public float GetPlayerWater()
        {
            return playerWater;
        }

        public void SetPlayerWater(float water)
        {
            playerWater = water;
            Hud.instance.UpdateWaterDisplay();
        }

        public void SaveWaterLevel(float water)
        {
            barrelWater = water;
        }

        public float GetWaterLevel()
        {
            return barrelWater;
        }
        #endregion
    }
}


