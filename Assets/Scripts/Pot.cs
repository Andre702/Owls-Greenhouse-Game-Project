using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pot : MonoBehaviour, IDropHandler
{
    private Image potPlantIcon;
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
                    // Maybe a standard interaction can be here?
                    break;
                
                case ItemType.Seed:
                    Seed seed = (Seed)itemReference;

                    potPlantIcon = transform.Find("Icon").GetComponent<Image>();
                    potPlantIcon.sprite = seed.seedSprite;
                    potPlantIcon.color = new Color(130f / 255f, 40f / 255f, 0f / 255f, 160f / 255f);

                    itemReference.EndUseItem(true);
                    break;

                case ItemType.Water:

                    // Add watering mechanic here.
                    // This will require a plant object as a child of the Pot.
                    itemReference.EndUseItem(true);
                    break;
            }
        }
    }
}
