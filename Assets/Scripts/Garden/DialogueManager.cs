using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public string[] lines;
    public float dialogueSpeed;

    protected int index;
    protected Coroutine typingCoroutine;

    public delegate void DialogueEventHandler();
    public static event DialogueEventHandler OnDialogueEnd;
    public static event DialogueEventHandler OnDialogueQuest;

    public virtual void Start()
    {
        Debug.Log("Dialoue Start Method");
        if (typingCoroutine == null)
        {
            index = 0;
            textBox.text = "";
            gameObject.SetActive(false);
        }
    }

    public virtual void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SkipDialogue();
        }
    }

    public void SkipDialogue()
    {
        if (textBox.text == lines[index])
        {
            NextLine();
        }
        else
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            textBox.text = lines[index];
        }
    }

    public virtual void BeginDialogue(string input)
    {
        Debug.Log("Dialoue Begun");
        lines = input.Split('|');
        index = 0;
        textBox.text = "";
        gameObject.SetActive(true);

        typingCoroutine = StartCoroutine(DisplayLine());
    }

    protected IEnumerator DisplayLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            yield return new WaitForSeconds(dialogueSpeed);
            textBox.text += c;
        }
    }

    public virtual void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textBox.text = "";
            if (lines[index].ToCharArray()[0] == '\u200B')
            {
                if (OnDialogueQuest != null)
                {
                    OnDialogueQuest();
                }
            }

            typingCoroutine = StartCoroutine(DisplayLine());
        }
        else
        {
            CloseDialogueBox();
        }
    }

    public void CloseDialogueBox()
    {
        textBox.text = "";
        lines = new string[3];
        gameObject.SetActive(false);
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        if (OnDialogueEnd != null)
        {
            OnDialogueEnd();
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    //This is the longest text that can be displayed in the dialogue box. It is quite long and right now I am writing more just because I have a space for it. The space is ending soon so aaaa
}
