using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite ogSprite;
    public Sprite hlSprite;
    // Apply proper sprites here ---------------------------------------------- ^^^

    private Image potPlantIcon;
    private int potIndex;

    private void Awake()
    {
        GetComponent<Image>().sprite = ogSprite;
    }

    private void Start()
    {
        // putting it in start just in case Unity is not done initializing indices in Awake.
        potIndex = transform.GetSiblingIndex();
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

                    GardenManager.instance.SetPlant(potIndex, seed.plantName, 1);

                    GardenManager.instance.PrintAllPlantData();

                    potPlantIcon = transform.Find("Icon").GetComponent<Image>();
                    potPlantIcon.sprite = seed.seedSprite;
                    potPlantIcon.color = new Color(130f / 255f, 40f / 255f, 0f / 255f, 160f / 255f);

                    

                    seed.EndUseItem(true);
                    break;

                case ItemType.Water:
                    Water water = (Water)itemReference;
                    // Add watering mechanic here.
                    // This will require a plant object as a child of the Pot.
                    water.EndUseItem(true);
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
