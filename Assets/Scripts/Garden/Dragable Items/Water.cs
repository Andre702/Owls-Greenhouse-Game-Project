using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : DragableItem
{
    public float waterLevel;

    private Image wateringCanImage;
    private Vector2 wateringCanDimensions;

    private AudioManagerGarden audioManager;

    protected override void Awake()
    {
        base.Awake();

        thisItemType = ItemType.Water;

        wateringCanImage = GetComponent<Image>();

        itemExplanation = $"This is a water barrel. Drag it on top of one of the plant to water it.|" +
            $"Keep track of the water level, in case you run out you will have to get more from a nearby lake.";
        
        float width = wateringCanImage.sprite.rect.width;
        float height = wateringCanImage.sprite.rect.height;
        wateringCanDimensions = new Vector2(width, height);
       
        canvasGroup.alpha = 0;
        // Watering can is invisible and it's hitbox is enlarged

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerGarden>();

    }

    protected override void BeginDragEffect()
    {
        base.BeginDragEffect();

        if (canBeUsed & waterLevel >= 20)    
        {
            audioManager.PlaySFX(audioManager.water_barrel);
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

    public void SetWaterLevel(float water)
    {
        if (water <= 100 && water >= 0)
        {
            waterLevel = water;
        }
        UpdateWaterLevel();
    }

    public float AddWater(float amountAdded)
    {
        waterLevel += amountAdded;
        if (waterLevel > 100)
        {
            float excessWater = waterLevel - 100;
            waterLevel = 100;
            UpdateWaterLevel();

            return excessWater;
        }
        else 
        {
            UpdateWaterLevel();
            return 0;
        }
        
    }

    public float GetWaterLevel()
    {
        return waterLevel;
    }
}
