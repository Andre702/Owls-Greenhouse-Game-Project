using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hartleaf : Plant
{
    //Needs:
    public bool needsWater = false;
    public bool needsToBeAlone = false;
    public bool needsToBeNearOther = false;
    public Hartleaf(int _index) : base(_index)
    {
        plantName = PlantName.Hartleaf;
        plantAge = 0;
        plantHealth = 5;
        isHappy = true;
        stage = 0;
    }

    public override void GrowEffectList()
    {
        switch (plantAge)
        {
            case 1:
                needsWater = true;
                needsToBeAlone = true;
                break;
            case 2:
                needsToBeAlone = true;
                break;
            case 3:
                stage += 1;
                needsToBeAlone = true;
                break;
            case 4:
                needsToBeAlone = true;
                break;
            case 6:
                stage += 1;
                break;
            case 10:
                stage += 1;
                needsWater = true;
                break;
            case 12:
                stage += 1;
                needsWater = true;
                needsToBeNearOther = true;
                break;
            case 13:
                needsToBeNearOther = true;
                break;
            case 14:
                needsToBeNearOther = true;
                stage += 1;
                break;
            default:
                //nothing special happens
                break;
        }

    }

    public override bool CheckHappiness()
    {
        if (needsWater)
        {
            return false;
        }
        if (needsToBeAlone)
        {
            return GameManager.instance.PlantNeedsCheck(this.index, PlantNeed.Alone);
        }
        if (needsToBeNearOther)
        {
            return GameManager.instance.PlantNeedsCheck(this.index, PlantNeed.NearOther);
        }

        return true;
        
    }
}
