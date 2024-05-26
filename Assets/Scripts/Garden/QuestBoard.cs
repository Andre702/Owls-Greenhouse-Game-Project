using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestBoard : MonoBehaviour
{
    public TextMeshProUGUI sunflowers;
    public TextMeshProUGUI sprillias;
    public TextMeshProUGUI heartleaf;

    public void SetupQuestBoard(int[] plantsNeeded)
    {
        sunflowers.text = $"0 / {plantsNeeded[0]}";
        sprillias.text = $"0 / {plantsNeeded[1]}";
        heartleaf.text = $"0 / {plantsNeeded[2]}";

    }

    public void UpdateQuestBoard(int[] adultPlants)
    {
        sunflowers.text = $"{adultPlants[0]} / 3";
        sprillias.text = $"{adultPlants[1]} / 2";
        heartleaf.text = $"{adultPlants[2]} / 1";
    }

}
