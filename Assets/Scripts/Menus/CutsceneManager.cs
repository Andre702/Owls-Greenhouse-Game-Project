using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public Animator animator;
    public DialogueManager dialogueBox;
    public GameObject greenhouseView;

    private bool dialogueFinished = false;

    void Start()
    {
        animator.SetTrigger("FadeIn");

        DialogueManager.OnDialogueEnd += FadeOut;
    }

    public void FadeOut()
    {
        dialogueFinished = true;
        animator.SetTrigger("FadeOut");
    }

    public void ProgressCutscene()
    {
        if (!dialogueFinished)
        {
            dialogueBox.BeginDialogue("Oh... What's that? A lost elvish child?|" +
            "I see You seek knowledge of botany. You wish to learn from me...|" +
            "Wery well. I shall take You as my apprentice, however...|" +
            "You need to prove to me that You are worthy of my knowledge.|" +
            "We will begin with a test. Come with me.");
        }
        else
        {
            NextScene();
        }
        

    }

    public void GreenhouseView()
    {
        Debug.Log("Greehouse view fade in");
        animator.SetTrigger("FadeIn");
        greenhouseView.SetActive(true);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDestroy()
    {
        DialogueManager.OnDialogueEnd -= FadeOut;
    }
}
