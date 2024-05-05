using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorImage : MonoBehaviour
{
    public Sprite[] cursorTypes;

    public Transform image;

    public void Enable(int cursorType)
    {
        image.GetComponent<Image>().sprite = cursorTypes[cursorType];
        gameObject.SetActive(true);
    }

    public void FollowMouse()
    {
        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = 9f;
        this.transform.position = Camera.main.ScreenToWorldPoint(cursorPosition);
    }
}
