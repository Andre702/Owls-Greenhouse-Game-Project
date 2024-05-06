using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shovel : DragableItem
{
    private Image shovelImage;
    private Vector2 shovelDimensions;
    private Quaternion ogRotation;

    protected override void Awake()
    {
        base.Awake();

        thisItemType = ItemType.Shovel;

        shovelImage = GetComponent<Image>();

        itemExplanation = $"This is a Shovel. Drag it on top of one of the plants to dig it up from its pot.\n" +
            $"Once you dug a plant you can replant it in another pot ot use left click to get rif of it.\n" +
            $"Be cautious as you can not replant withered plants!";


        float width = shovelImage.sprite.rect.width;
        float height = shovelImage.sprite.rect.height;
        shovelDimensions = new Vector2(width, height);
        ogRotation = shovelImage.rectTransform.rotation;

        canvasGroup.alpha = 0;
        

        pickupSound = "Clank"; // Replace those with actual sounds xd
        useSound = "Kshch";
    }

    protected override void BeginDragEffect()
    {
        base.BeginDragEffect();

        if (canBeUsed)
        {
            RectTransform rectTransform = GetComponent<Image>().rectTransform;
            rectTransform.sizeDelta = shovelDimensions;
            rectTransform.rotation = Quaternion.Euler(0, 0, 0);
            canvasGroup.alpha = 1;
        }

        BeginDragResult();
        
    }

    protected override void EndDragEffect()
    {
        canvasGroup.alpha = 0;
        RectTransform rectTransform = GetComponent<Image>().rectTransform;
        rectTransform.rotation = ogRotation;
        base.EndDragEffect();
    }
}
