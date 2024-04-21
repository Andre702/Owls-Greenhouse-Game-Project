using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using System.Reflection;
using System;
using UnityEngine.SceneManagement;

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
        GameManager.instance.StopTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}