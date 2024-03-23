using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : Plant
{
    public Sunflower(int _index) : base(_index)
    {
        plantName = PlantName.Sunflower;
        plantAge = 0;
        plantHealth = 4;
        isHappy = true;
    }

    public override void GrowEffectList()
    {
        switch (plantAge)
        {
            case 2:
                IncrementStage();
                break;
            case 4:
                IncrementStage();
                break;
            case 6:
                IncrementStage();
                break;
            case 8:
                IncrementStage();
                break;
            case 10:
                IncrementStage();
                break;
            default:
                //nothing special happens
                break;
        }

    }
}
