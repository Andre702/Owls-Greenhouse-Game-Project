using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBase;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // This class is used for functions used in both Garden and Forest

    public delegate bool HudItemUseHandler();
    public static event HudItemUseHandler OnSkipTime;
    public static event HudItemUseHandler OnWaterUse;

    public static GameManager instance { get; private set; }
    // instance of GameManager needs to be accessible from every scene
    // as the plants need to be constantly growing throughout the game

    public float secondsInHour = 10;
    private bool timeIsFlowing = false;
    private float currentTime = 0f;

    public int[] quest = { 3, 2, 1 };
    public QuestBoard questBoard;

    public CanvasGroup nightShade;

    public bool isSceneGarden = true;

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
        if (GameData.instance.gameStartedFirstTime)
        {
            DialogueManager.OnDialogueQuest += QuestDisplayObjective;
        }
    }

    private void Update()
    {
        if (timeIsFlowing)
        {
            currentTime += Time.deltaTime;
        }

        if (currentTime >= secondsInHour)
        {
            currentTime = 0f;
            TimeNextHour();

            Debug.Log("Hour passes");
        }
    }

    public void LockAllFunctionalities(bool locked)
    {
        nightShade.GetComponent<Image>().raycastTarget = locked;
    }

    public bool QuestUpdateCondition()
    {
        int[] currentState = GameData.instance.CountAdultPlants();

        if (currentState.Length != quest.Length)
        {
            Debug.LogWarning("Lengths of quest array and current state array differ! If new plant was added check the arrays for missing spot!");
            return false;
        }

        questBoard.UpdateQuestBoard(currentState);

        bool endFlag = true;
        for (int i = 0; i < quest.Length; i++)
        {
            if (currentState[i] < quest[i])
            {
                endFlag = false;
                break;
            }
        }

        if (endFlag)
        {
            return true;
        }

        return false;
    }

    public void QuestDisplayObjective()
    {
        GameData.instance.gameStartedFirstTime = false;
        questBoard.SetupQuestBoard(quest);
        QuestBoardShow(true);
        DialogueManager.OnDialogueQuest -= QuestDisplayObjective;
    }

    public void TimeInvokeSkip()
    {
        if (OnSkipTime != null)
        {
            if (OnSkipTime())
            {
                TimeNextHour();
            }
            else
            {
                Debug.Log("Time Skip prevented by event from Garden");
            }
        }
        else
        {
            TimeNextHour();
        }
    }

    public void WaterInvokeUse()
    {
        if (OnWaterUse != null)
        {
            if (OnWaterUse())
            {
                Debug.Log("Water should have been poured.");
            }
            else
            {
                Debug.Log("Time Skip prevented by event from Garden");
            }
        }
        else
        {
            
        }
    }

    public void TimeNextHour()
    {
        if (GameData.instance.hour <= 24)
        {
            GameData.instance.IncrementHour();
            nightShade.alpha += 0.02f;
            PlantAllGrow();
            PlantAllCheckHappiness();
            Hud.instance.UpdateHourDisplay();
            if (QuestUpdateCondition())
            {
                EndGame(false);
            }
        }
        else
        {
            EndGame(true);
        }
        
    }

    public void TimeStart()
    {
        timeIsFlowing = true;
    }

    public void TimeStop()
    {
        timeIsFlowing = false;
        currentTime = 0f;
    }

    public bool PlantNeedsCheck(int plantIndex, PlantNeed need)
    {

        switch (need)
        {
            case PlantNeed.Alone:
                if (GameData.instance.GetPlantData(plantIndex - 1).plantName == PlantName.EMPTY &&
                    GameData.instance.GetPlantData(plantIndex + 1).plantName == PlantName.EMPTY
                    )
                {
                    return true;
                }
                return false;
            case PlantNeed.NearOther:
                if (GameData.instance.GetPlantData(plantIndex - 1).plantName == PlantName.EMPTY &&
                    GameData.instance.GetPlantData(plantIndex + 1).plantName == PlantName.EMPTY
                    )
                {
                    return false;
                }
                return true;
            default:
                Debug.LogWarning("Need not implemented yet!");
                return false;

        }
    }

    public void PlantAllGrow(int growth = 1)
    {
        foreach (var plantData in GameData.instance.GetAllPlants())
        {
            if (plantData.plantName != PlantName.EMPTY)
            {
                plantData.Grow();
                plantData.UpdateVisuals();
            }
        }
    }

    public void PlantAllCheckHappiness(int growth = 1)
    {
        foreach (var plantData in GameData.instance.GetAllPlants())
        {
            if (plantData.plantName != PlantName.EMPTY)
            {
                plantData.CheckHappiness();
            }
        }
    }

    public void SceneChangeForest()
    {
        TimeStart();
        QuestBoardShow(false);
        Hud.instance.ButtonsInteractable(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SceneChangeGreenhouse()
    {
        TimeStop();
        Hud.instance.ButtonsInteractable(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); 
    }

    public void SceneChangeEndScreen()
    {
        SceneManager.LoadScene(4);
        Hud.instance.gameObject.SetActive(false);
        nightShade.alpha = 0;
        QuestBoardShow(false);
    }

    public void QuestBoardShow(bool show)
    {
        questBoard.gameObject.SetActive(show);
    }

    private void EndGame(bool forced)
    {
        TimeStop();
        Hud.instance.ButtonsInteractable(false);
        

        GameData.instance.gameFinished = true;

        if (forced && SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(2);
        }

    }
}
