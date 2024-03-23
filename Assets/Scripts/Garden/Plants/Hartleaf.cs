using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hartleaf : Plant
{
    public Hartleaf(int _index) : base(_index)
    {
        plantName = PlantName.Hartleaf;
        plantAge = 0;
        plantHealth = 5;
        isHappy = true;
    }

    public override void GrowEffectList()
    {
        switch (plantAge)
        {
            case 3:
                IncrementStage();
                break;
            case 6:
                IncrementStage();
                break;
            case 10:
                IncrementStage();
                break;
            case 12:
                IncrementStage();
                break;
            case 14:
                IncrementStage();
                break;
            default:
                //nothing special happens
                break;
        }

    }
}
