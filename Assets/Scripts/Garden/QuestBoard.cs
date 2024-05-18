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
        sunflowers.text.ToCharArray()[0] = (char)adultPlants[0];
        sprillias.text.ToCharArray()[0] = (char)adultPlants[1];
        heartleaf.text.ToCharArray()[0] = (char)adultPlants[2];
    }

    public void ShowQuestBoard(bool show)
    {
        this.gameObject.SetActive(show);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
