using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Seed : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 screen_position;
    public Sprite seedSprite;

    private Transform originalSlot;
    private CanvasGroup canvasGroup;
    private bool locked = false;

    void Awake()
    {
        seedSprite = GetComponent<Image>().sprite;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalSlot = transform.parent; // remember original slot
        transform.SetParent(transform.parent.parent.parent); // chage parent temporarily
        // This does not look pretty but 3 parents up the seed should be just assigned to canvas

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        screen_position = Input.mousePosition;
        screen_position.z = Camera.main.nearClipPlane + 8;
        transform.position = Camera.main.ScreenToWorldPoint(screen_position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalSlot); // snap back

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

}
