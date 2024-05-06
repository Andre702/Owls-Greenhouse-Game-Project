using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Owl : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GardenManager.instance.CursorSetQuestion();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OWL!");
        GetComponent<Image>().color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.white;
    }
}
