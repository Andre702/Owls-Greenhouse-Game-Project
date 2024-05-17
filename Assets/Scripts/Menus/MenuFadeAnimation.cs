using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFadeAnimation : MonoBehaviour
{
    public MainMenuLogic mainController;
    public void OnFadeComplete()
    {
        mainController.StartGame();
    }

}
