using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDialogueBox : DialogueManager
{
    public bool dialogueOpen = false;

    public override void Start()
    {
        textBox.text = "";
        dialogueOpen = false;
    }

    public override void Update()
    {
        
    }

    public override void BeginDialogue(string input)
    {
        base.BeginDialogue(input);

        dialogueOpen = true;
    }

    public override void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textBox.text = "";
            typingCoroutine = StartCoroutine(DisplayLine());

        }
        else
        {
            textBox.text = "";
            lines = new string[3];
        }
    }


}
