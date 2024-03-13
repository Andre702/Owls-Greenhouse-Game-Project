using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ItemType
{
    Seed,
    Water
}

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 screen_position;
    public ItemType thisItemType { get; protected set; }
    [HideInInspector]
    public bool canBeUsed = true;
    public Sprite ogParentSprite;
    public Sprite hlParentSprite;

    protected Transform ogParent; 
    protected CanvasGroup canvasGroup;
    protected string pickupSound;
    protected string useSound;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        ogParent = transform.parent; // remember original parent
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragEffect();

        if (canBeUsed)
        {
            // og parent used to be assigned here (hopefully won't cause bugs)
            transform.SetParent(GameObject.Find("UICanvas").transform); // chage parent temporarily

            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canBeUsed)
        {
            screen_position = Input.mousePosition;
            screen_position.z = Camera.main.nearClipPlane + 8;
            transform.position = Camera.main.ScreenToWorldPoint(screen_position);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canBeUsed)
        {
            transform.SetParent(ogParent); // snap back

            canvasGroup.blocksRaycasts = true;

            EndDragEffect();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("MOUSE ON!");
        ogParent.GetComponent<Image>().sprite = hlParentSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("MOUSE OFF!");
        ogParent.GetComponent<Image>().sprite = ogParentSprite;
    }

    // Virtual:

    protected virtual void BeginDragEffect() // Executes at the beginnin of Dragging, can be overridden by Dragable objects
    {
        if (canBeUsed) 
        {
            Debug.Log("Play sound" + pickupSound); 
        }
        else 
        { 
            Debug.Log("This " + thisItemType + " can not be used.");
        }
    } 

    protected virtual void EndDragEffect() // Executes at the end of Dragging, can be overridden by Dragable objects
    {

    }

    public virtual void EndUseItem(bool wasUsed) // Executed by the interacted space when the item has been dropped on it
    {
        if (wasUsed)
        {
            Debug.Log("Play sound" + useSound);
        }
    }
}
