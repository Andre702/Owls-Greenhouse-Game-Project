using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public string[] lines;
    public float dialogueSpeed;

    protected int index;
    protected Coroutine typingCoroutine;

    public virtual void Start()
    {
        index = 0;
        gameObject.SetActive(false);
        textBox.text = "";     
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
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        textBox.text = "";
        lines = input.Split('|');
        index = 0;
        gameObject.SetActive(true);

        Debug.Log("BeginCoroutine");
        typingCoroutine = StartCoroutine(DisplayLine());
    }

    protected IEnumerator DisplayLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textBox.text += c;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    public virtual void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textBox.text = "";
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
    }

    private void OnDestroy()
    {
        CloseDialogueBox();
    }

    //This is the longest text that can be displayed in the dialogue box. It is quite long and right now I am writing more just because I have a space for it. The space is ending soon so aaaa
}
