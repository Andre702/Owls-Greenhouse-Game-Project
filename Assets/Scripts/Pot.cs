using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pot : MonoBehaviour, IDropHandler
{
    private Image plantIcon;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            Seed seedReference = dropped.GetComponent<Seed>();
            
            if (seedReference != null)
            {
                plantIcon = transform.Find("Icon").GetComponent<Image>();
                plantIcon.sprite = seedReference.seedSprite;
                plantIcon.color = new Color(130f / 255f, 40f / 255f, 0f / 255f, 160f / 255f);
            }
        }
    }
}
