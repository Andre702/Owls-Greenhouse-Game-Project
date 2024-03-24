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
    public PlantImage potPlantImage;
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
                        isEmpty = false;
                        GardenManager.instance.PlantPlant(potIndex, seed.plantName);

                        Image plantIcon = potPlantIcon.GetComponent<Image>();
                        plantIcon.sprite = seed.seedSprite;
                        plantIcon.color = new Color(130f / 255f, 40f / 255f, 0f / 255f, 160f / 255f);

                        seed.EndUseItem(true);
                        break;
                    }
                    seed.EndUseItem(false);
                    break;

                case ItemType.Water:
                    Water water = (Water)itemReference;
                    // Add watering mechanic here.
                    // This will require a plant object as a child of the Pot.
                    water.EndUseItem(true);
                    break;

                case ItemType.Shovel:
                    Shovel shovel = (Shovel)itemReference;
                    if (!isEmpty)
                    {
                        GardenManager.instance.PlantDigUp(potIndex);
                        isEmpty = true;

                        Image plantIcon = potPlantIcon.GetComponent<Image>();
                        plantIcon.sprite = null;
                        plantIcon.color = new Color(0f / 0f, 0f / 0f, 0f / 0f, 0f / 0f);
                        break;
                    }
                    shovel.EndUseItem(false);
                    break;

            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = hlSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = ogSprite;
    }
}
