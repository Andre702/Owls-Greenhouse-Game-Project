using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : CutsceneManager
{
    public GameObject theEndTitle;
    public GameObject flower;
    private bool gameSuccessful = false;

    private string endScreenDialogue;

    private void Start()
    {
        gameSuccessful = GameManager.instance.QuestUpdateCondition();

        if (gameSuccessful)
        {
            flower.SetActive(true);

            endScreenDialogue = "You begun studies beside you master the great Owl.\n" +
                "You set out to become the greatest botanist that ever lived.|" +
                "After many yers and many more tests you became a master of the green world, a beacon of science and discovery";
        }
        else
        {
            endScreenDialogue = "You failed to satisfy you teacher desires, however your hopes have not been extinguished.|" +
                "Some day You will try again and show the great Owl what you can really do.";
        }
    }

    public override void ProgressCutscene()
    {
        if (!dialogueFinished)
        {
            dialogueBox.BeginDialogue(endScreenDialogue);
        }
        else
        {
            
        }
    }

    public override void FinishCutscene()
    {
        theEndTitle.SetActive(true);
    }


}
