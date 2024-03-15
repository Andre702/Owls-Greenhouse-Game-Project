using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Planter
{

    public static void Plant(Pot pot, PlantName name)
    {
        GameObject plantPrefab = Resources.Load<GameObject>(name.ToString());

        if(plantPrefab == null)
        {
            Debug.Log("Could not find plant prefab named: " + name.ToString());
        }
        else
        {
            GameObject newPlant = plantPrefab;
            newPlant.transform.SetParent(pot.transform);
        }

    }
}
