using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Seed : DragableItem
{

    public PlantName plantName;

    private bool unlocked;

    private AudioManagerGarden audioManager;

    protected override void Awake()
    {
        base.Awake();
        thisItemType = ItemType.Seed;

        itemExplanation = $"This is a {plantName.ToString()} seed. Drag and drop it into one of the pots to plant it." +
            $"Take good care of each and every plant you grow. |" +
            $"Remember to speak with them often to learn about their needs.";
        
        canBeUsed = true; // Seeds need to check if they are unlocked at this game level
                          // and then determine weather or not they can be used yet

        
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerGarden>();


        unlocked = true; // This will depend on the world script and state of the game
    }

    protected override void BeginDragEffect()
    {
        base.BeginDragEffect();

        if (canBeUsed & unlocked)
        {
            audioManager.PlaySFX(audioManager.planting);
            canvasGroup.alpha = 0.6f;
        }

        BeginDragResult();
        
    }

    protected override void EndDragEffect()
    {
        canvasGroup.alpha = 1;
        base.EndDragEffect();
    }

    public Sprite GetSprite()
    {
        return GetComponent<Image>().sprite;
    }
}
