using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public string[] lines;
    public float dialogueSpeed;

    public bool stopDialogue = false;

    private int index;
    private Coroutine typing;

    void Start()
    {
        gameObject.SetActive(false);
        textBox.text = "";     
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (textBox.text == lines[index])
            {
                NextLine();
            }
            else
            {
                if (typing != null)
                {
                    StopCoroutine(typing);
                }
                textBox.text = lines[index];
            }
        }
    }

    public void BeginDialogue(string input)
    {
        lines = input.Split('\n');
        index = 0;
        gameObject.SetActive(true);

        typing = StartCoroutine(DisplayLine());

    }

    IEnumerator DisplayLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textBox.text += c;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    void NextLine()
    {
        stopDialogue = false;
        if (index < lines.Length - 1)
        {
            index++;
            textBox.text = "";
            typing = StartCoroutine(DisplayLine());
            
            //if (typing == null)
            //{

            //}
            
        }
        else
        {
            textBox.text = "";
            lines = new string[3];
            gameObject.SetActive(false);
            if (typing != null)
            {
                StopCoroutine(typing);
            }
        }
    }

    //This is the longest text that can be displayed in the dialogue box. It is quite long and right now I am writing more just because I have a space for it. The space is ending soon so aaaa
}
