using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GreenhouseCursor : MonoBehaviour
{
    public Sprite[] cursorTypes;
   
    public void EnableCursor(int cursorIndex)
    {
        GetComponent<Image>().sprite = cursorTypes[cursorIndex];
        Update();
        gameObject.SetActive(true);
    }

    void Update()
    {
        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = 9f;
        this.transform.position = Camera.main.ScreenToWorldPoint(cursorPosition);

        if (Input.GetMouseButtonDown(1))
        {
            DisableCursor();
            return;
        }
    }

    public void DisableCursor()
    {
        GardenManager.instance.ClearCursor();
        gameObject.SetActive(false);
    }
}
