using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 screen_position;
    Transform originalSlot;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        originalSlot = transform.parent;
        transform.SetParent(transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(Input.mousePosition);
        screen_position = Input.mousePosition;
        screen_position.z = Camera.main.nearClipPlane + 8;
        transform.position = Camera.main.ScreenToWorldPoint(screen_position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(originalSlot);
    }

}
