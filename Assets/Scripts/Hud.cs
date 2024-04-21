using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using TMPro;

public class Hud : MonoBehaviour
{
    public static Hud instance { get; private set; }

    public TMP_Text clock;
    public TMP_Text jar;


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
        UpdateHourDisplay();


    }

    public void UpdateHourDisplay()
    {
        clock.text = GameData.instance.GetHour().ToString();
    }

    public void UpdateWaterDisplay()
    {
        jar.text = GameData.instance.GetHour().ToString();
    }
}
