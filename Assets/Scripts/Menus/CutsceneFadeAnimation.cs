using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneFadeAnimation : MonoBehaviour
{
    public CutsceneManager cutsceneManager;
    public void OnFadeInComplete()
    {
        cutsceneManager.ProgressCutscene();
    }

    public void OnFadeOutComplete()
    {
        cutsceneManager.GreenhouseView();
    }

}
