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

    private Image potPlantIcon;
    private int potIndex;
    private bool isEmpty = true;

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
                    if (isEmpty)
                    {
                        Seed seed = (Seed)itemReference;
                        CreatePlant(seed.plantName);
                        GardenManager.instance.PlantPlant(potIndex, seed.plantName);

                        potPlantIcon = transform.Find("Icon").GetComponent<Image>();
                        potPlantIcon.sprite = seed.seedSprite;
                        potPlantIcon.color = new Color(130f / 255f, 40f / 255f, 0f / 255f, 160f / 255f);



                        seed.EndUseItem(true);
                        break;
                    }
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

    private void CreatePlant(PlantName name)
    {
        GameObject plantPrefab = Resources.Load<GameObject>(name.ToString());

        if (plantPrefab == null)
        {
            Debug.Log("Could not find plant prefab named: " + name.ToString());
        }
        else
        {
            Debug.Log("Trying to instantiate object from prefab: " + name.ToString());
            GameObject newPlant = Instantiate(plantPrefab, this.transform, false);
            newPlant.transform.SetParent(this.transform);
        }
    }
}
