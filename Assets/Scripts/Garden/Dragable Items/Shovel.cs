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
