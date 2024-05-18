using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using System.Reflection;
using System;

public class ForestManager : MonoBehaviour
{
    public static ForestManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    public void GoGreenhouse()
    {
        GameManager.instance.SceneChangeGreenhouse();
    }
}