using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorImage : MonoBehaviour
{
    public Sprite[] cursorTypes;

    public void Enable(int cursorType)
    {
        GetComponent<Image>().sprite = cursorTypes[cursorType];
    }

    public void FollowMouse()
    {
        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = 9f;
        this.transform.position = Camera.main.ScreenToWorldPoint(cursorPosition);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
