using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : CutsceneManager
{
    public GameObject theEndTitle;
    public Animator titleAnimator;
    public GameObject flower;
    private bool gameSuccessful = false;

    private string endScreenDialogue;

    private void Start()
    {
        gameSuccessful = GameManager.instance.QuestUpdateCondition();
        animator.SetTrigger("FadeIn");
        DialogueManager.OnDialogueEnd += ProgressCutscene;

        if (gameSuccessful)
        {
            flower.SetActive(true);

            endScreenDialogue = "You begun studies beside your master the great Owl.\n" +
                "You set out to become the greatest botanist that ever lived.|" +
                "After many years and many more tests you became a master of the green world, a beacon of science and discovery";
        }
        else
        {
            endScreenDialogue = "You failed to satisfy your teacher's desires, however your hopes have not been extinguished.|" +
                "Some day you will try again and show the great Owl what you can really do.";
        }
    }

    public override void ProgressCutscene()
    {
        if (!dialogueFinished)
        {
            dialogueFinished = true;
            dialogueBox.BeginDialogue(endScreenDialogue);
        }
        else
        {
            titleAnimator.SetTrigger("FadeIn");
            theEndTitle.SetActive(true);
        }
    }

    public override void FinishCutscene()
    {
        theEndTitle.SetActive(true);
    }


}
