using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PlantName
{
    Sunflower,
    Sprillia,
    Hartleaf
}

public class Seed : DragableItem
{
    public Sprite seedSprite;
    public PlantName plantName;

    private bool unlocked;

    protected override void Awake()
    {
        base.Awake();
        seedSprite = GetComponent<Image>().sprite;
        thisItemType = ItemType.Seed;

        canBeUsed = true; // Seeds need to check if they are unlocked at this game level
                          // and then determine weather or not they can be used yet

        pickupSound = "Hszz"; // Replace those with actual sounds xd
        useSound = "Blink";

        unlocked = true; // This will depend on the world script and state of the game
    }

    private void Start()
    {
        // plant name depends on where you put it.
        // The order of seeds should be the same as the order of plant names in the enum
        plantName = (PlantName)transform.parent.transform.GetSiblingIndex();
    }

    protected override void BeginDragEffect()
    {
        if (unlocked)
        {
            canBeUsed = true;

            canvasGroup.alpha = 0.6f;
        }
        else
        {
            canBeUsed = false;
        }
        base.BeginDragEffect();
    }

    protected override void EndDragEffect()
    {
        canvasGroup.alpha = 1;
        base.EndDragEffect();
    }
}
