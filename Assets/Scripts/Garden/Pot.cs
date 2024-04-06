using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Reflection;

public class Pot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite ogSprite;
    public Sprite hlSprite;
    public Transform potPlantIcon;
    public int potIndex;

    private bool isEmpty = true;

    private void Awake()
    {
        GetComponent<Image>().sprite = ogSprite;

    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            DragableItem itemReference = dropped.GetComponent<DragableItem>();
            
            if (!itemReference.canBeUsed) { return; }

            switch (itemReference.thisItemType)
            {
                default:
                    Debug.Log("Unknown item used on a pot");
                    break;
                
                case ItemType.Seed:
                    Seed seed = (Seed)itemReference;
                    if (isEmpty)
                    {
                        GardenManager.instance.PlantPlant(potIndex, seed.plantName);
                        PlantPlant(seed.GetSprite());
                        seed.EndUseItem(true);
                        break;
                    }
                    seed.EndUseItem(false);
                    break;

                case ItemType.Water:
                    Water water = (Water)itemReference;
                    water.EndUseItem(GardenManager.instance.PlantAttemptToWater(potIndex));
                    // returns itemUsed = true if the plant was successfully watered
                    break;

                case ItemType.Shovel:
                    Shovel shovel = (Shovel)itemReference;
                    if (!isEmpty)
                    {
                        GardenManager.instance.PlantDigUp(potIndex);
                        RemovePlant();
                        break;
                    }
                    shovel.EndUseItem(false);
                    break;

            }
        }
    }

    public void PlantPlant(Sprite icon)
    {
        if (icon == null) { return; }

        isEmpty = false;

        Image plantIcon = potPlantIcon.GetComponent<Image>();
        plantIcon.sprite = icon;
        plantIcon.color = new Color(130f / 255f, 40f / 255f, 0f / 255f, 160f / 255f);
    }

    public void RemovePlant()
    {
        isEmpty = true;

        Image plantIcon = potPlantIcon.GetComponent<Image>();
        plantIcon.sprite = null;
        plantIcon.color = new Color(0f / 0f, 0f / 0f, 0f / 0f, 0f / 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = hlSprite;
        GardenManager.instance.cursor.target = potIndex;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = ogSprite;
        GardenManager.instance.cursor.target = -1;
    }
}
