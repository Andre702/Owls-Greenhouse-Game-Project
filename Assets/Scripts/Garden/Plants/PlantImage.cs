using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantImage : MonoBehaviour
{
    private Sprite[] spriteSheet;

    public void EnablePlant(Sprite[] _spriteSheet, bool isHappy, int stage = 0) 
    {
        if (_spriteSheet != null)
        {
            spriteSheet = _spriteSheet;
            UpdateCurrentSprite(stage, isHappy);
            gameObject.SetActive(true);
        }
        else
        {
            GetComponent<Image>().sprite = null;
        }
    }
    // Shows plant which was hidden if a sprite sheet was set for it. (Uses UpdateCurrentSprite)
    // Set custom stage when returning to the Greenhouse after couple hours in the Forest.

    public void UpdateCurrentSprite(int stage, bool isHappy)
    {

        transform.GetChild(0).gameObject.SetActive(!isHappy);
        if (stage < 0)
        {
            return;
        }

        if (spriteSheet != null)
        {
            if (stage < spriteSheet.Length)
            {
                GetComponent<Image>().sprite = spriteSheet[stage];
                return;
            }
            else
            {
                Debug.LogWarning("Trying to set plant stage that exceeds this plant's current sprite sheet");
            }
        }
        else
        {
            Debug.LogWarning("Trying to set plant sprite when no sprite sheet applied");
        }
    }
    // Used to quickly update Plant sprite from current sprite.
    // This could use an exception system, with it it could display index of plant throwing exceptions

    public void ChangeImageToDead(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    // Sets custom sprite for a plant
    // Could be redundant if every sprite sheet will include 2 more stages: dead and empty

    public void DisablePlant()
    {
        //spriteSheet = null;
        gameObject.SetActive(false);
    }
    // Hides plant.
}
 