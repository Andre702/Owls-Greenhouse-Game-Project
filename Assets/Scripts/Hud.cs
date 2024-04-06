using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public static Hud instance { get; private set; }
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

    public Transform clock;
    public Transform jar;

    public void UpdateHourDisplay(int hour)
    {
        clock.GetChild(0).GetComponent<TextMesh>().text = hour.ToString();
    }
}
