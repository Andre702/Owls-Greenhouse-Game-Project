using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantImage : MonoBehaviour
{
    private Sprite[] spriteSheet;

    public void EnablePlant(Sprite[] _spriteSheet, int stage = 0) 
    {
        if (_spriteSheet != null)
        {
            spriteSheet = _spriteSheet;
            UpdateSprite(stage);
            gameObject.SetActive(true);
        }
    }
    // Shows plant which was hidden if a sprite sheet was set for it. (Uses UpdateSprite)
    // Set custom stage when returning to the Greenhouse after couple hours in the Forest.

    public void UpdateSprite(int stage)
    {
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

    public void SetSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
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
 