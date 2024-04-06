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
        stage = 0;
    }

    public override void GrowEffectList()
    {
        switch (plantAge)
        {
            case 2:
                stage += 1;
                break;
            case 4:
                stage += 1;
                isHappy = false;
                break;
            case 6:
                stage += 1;
                break;
            case 8:
                stage += 1;
                isHappy = false;
                break;
            case 10:
                stage += 1;
                break;
            default:
                //nothing special happens
                break;
        }

    }
}
