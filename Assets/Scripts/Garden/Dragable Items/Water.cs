using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : DragableItem
{
    public float waterLevel = 100;

    private Image wateringCanImage;
    private Vector2 wateringCanDimensions;


    protected override void Awake()
    {
        base.Awake();

        thisItemType = ItemType.Water;

        wateringCanImage = GetComponent<Image>();

        float width = wateringCanImage.sprite.rect.width;
        float height = wateringCanImage.sprite.rect.height;
        wateringCanDimensions = new Vector2(width, height);
       
        canvasGroup.alpha = 0;
        // Watering can is invisible and it's hitbox is enlarged

        pickupSound = "Flop"; // Replace those with actual sounds xd
        useSound = "Flush";

        UpdateWaterLevel();

    }

    protected override void BeginDragEffect()
    {
        base.BeginDragEffect();

        if (canBeUsed & waterLevel > 10)    
        {
            RectTransform rectTransform = wateringCanImage.rectTransform;
            rectTransform.sizeDelta = wateringCanDimensions;
            canvasGroup.alpha = 1;
        }

        BeginDragResult();
    }

    protected override void EndDragEffect()
    {
        canvasGroup.alpha = 0;
        base.EndDragEffect();
    }

    public override void EndUseItem(bool wasUsed)
    {
        base.EndUseItem(wasUsed);

        if (wasUsed)
        {
            waterLevel -= 20;
            UpdateWaterLevel();
        }
    }

    private void UpdateWaterLevel()
    {
        float waterPercentage = waterLevel / 100f;
        Debug.Log(waterLevel / 100);
        GameObject.Find("WaterLevel").transform.localScale = new Vector3(1, waterPercentage, 1);
    }

    public void AddWater(int amount)
    {
        if (waterLevel + amount <= 100)
        {
            waterLevel += amount;
        }
        else 
        {
            waterLevel = 100;
        }
    }

}
