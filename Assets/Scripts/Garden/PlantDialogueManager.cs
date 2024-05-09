using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantDialogueManager : MonoBehaviour
{
    public Transform plantIcon;
    public PlantDialogueBox dialogueBox;
    public Plant plantTalking;

    private bool dialogueJustStarted = false;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && dialogueBox.dialogueOpen)
        {
            if (dialogueJustStarted)
            {
                dialogueJustStarted = false;
                return;
            }
            dialogueBox.SkipDialogue();
            dialogueBox.dialogueOpen = false;
        }
    }

    public void BeginDialogue(Plant plant, Sprite icon)
    {
        plantIcon.GetComponent<Image>().sprite = icon;

        plantTalking = plant;

        gameObject.SetActive(true);
        dialogueBox.BeginDialogue(plantTalking.FormDialogue(0));
    }

    public void AskQuestionFeel()
    {
        if (dialogueBox.dialogueOpen == false)
        {
            dialogueJustStarted = true;
            dialogueBox.BeginDialogue(plantTalking.FormDialogue(1));
        }
        
    }

    public void AskQuestionNeeds()
    {
        if (dialogueBox.dialogueOpen == false)
        {
            dialogueJustStarted = true;
            dialogueBox.BeginDialogue(plantTalking.FormDialogue(2));
        }
    }

    public void AskQuestionAge()
    {
        if (dialogueBox.dialogueOpen == false)
        {
            dialogueJustStarted = true;
            dialogueBox.BeginDialogue(plantTalking.FormDialogue(3));
        }
    }

    public void CloseDialogueManager()
    {
        dialogueBox.CloseDialogueBox();
        dialogueBox.dialogueOpen = false;
        this.plantIcon.GetComponent<Image>().sprite = null;
        this.plantTalking = null;
        gameObject.SetActive(false);
    }

}
