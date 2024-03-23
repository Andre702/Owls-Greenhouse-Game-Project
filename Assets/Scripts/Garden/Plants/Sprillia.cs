using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprillia : Plant
{
    public Sprillia(int _index) : base(_index)
    {
        plantName = PlantName.Sprillia;
        plantAge = 0;
        plantHealth = 3;
        isHappy = true;
    }

    public override void GrowEffectList()
    {
        switch (plantAge)
        {
            case 3:
                IncrementStage();
                break;
            case 5:
                IncrementStage();
                break;
            case 8:
                IncrementStage();
                break;
            case 10:
                IncrementStage();
                break;
            case 13:
                IncrementStage();
                break;
            default:
                //nothing special happens
                break;
        }

    }
}
