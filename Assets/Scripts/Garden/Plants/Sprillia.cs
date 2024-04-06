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
                stage += 1;
                break;
            case 5:
                stage += 1;
                break;
            case 8:
                stage += 1;
                isHappy = false;
                break;
            case 10:
                stage += 1;

                break;
            case 13:
                stage += 1;
                break;
            default:
                //nothing special happens
                break;
        }

    }
}
