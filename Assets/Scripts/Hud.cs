using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using TMPro;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public static Hud instance { get; private set; }

    public TMP_Text clockText;
    public TMP_Text jarText;

    public Button clock;
    public Button jar;


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

    private void Start()
    {
        UpdateHourDisplay();
        UpdateWaterDisplay();
    }

    public void UpdateHourDisplay()
    {
        clockText.text = GameData.instance.GetHour().ToString();
    }

    public void UpdateWaterDisplay()
    {
        jarText.text = GameData.instance.getPlayerWater().ToString();
    }

    public void ButtonsInteractable(bool state)
    {
        clock.interactable = state;
        jar.interactable = state;
    }

    // how to invoke next hour with button input when I also want to ask questions about it?

    // got it: Whenever button is clicked perform an event function in GameManager to which GarderManager is subscribed to
    // whenever GameManager is performing a question it will also return false in the event itself.
    // event function will not proceede if false was returned. 
    // if GardenManager is not present it will not be subscribed to the event so the default true will proceede.

}
