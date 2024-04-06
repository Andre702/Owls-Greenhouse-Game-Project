using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Seed : DragableItem
{
    public PlantName plantName;

    private bool unlocked;

    protected override void Awake()
    {
        base.Awake();
        thisItemType = ItemType.Seed;

        canBeUsed = true; // Seeds need to check if they are unlocked at this game level
                          // and then determine weather or not they can be used yet

        pickupSound = "Hszz"; // Replace those with actual sounds xd
        useSound = "Blink";

        unlocked = true; // This will depend on the world script and state of the game
    }

    protected override void BeginDragEffect()
    {
        base.BeginDragEffect();

        if (canBeUsed & unlocked)
        {
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
