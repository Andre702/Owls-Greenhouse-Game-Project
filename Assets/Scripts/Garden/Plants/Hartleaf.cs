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
        stage = 0;
    }

    public override void GrowEffectList()
    {
        switch (plantAge)
        {
            case 1:
                isHappy = false;
                break;
            case 3:
                stage += 1;
                break;
            case 6:
                stage += 1;
                break;
            case 10:
                stage += 1;
                isHappy = false;
                break;
            case 12:
                stage += 1;
                isHappy = false;
                break;
            case 14:
                stage += 1;
                break;
            default:
                //nothing special happens
                break;
        }

    }
}
