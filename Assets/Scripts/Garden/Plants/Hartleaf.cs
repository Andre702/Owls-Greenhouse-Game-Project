using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hartleaf : Plant
{
    public Hartleaf() : base()
    {
        plantName = PlantName.Hartleaf;
        growthState = 0;
        health = 5;
        isHappy = true;
    }

    public override void GrowEffect(int growState)
    {
        switch (growState)
        {
            case 3:
                //Need water?
                break;
            case 7:
                //water again?
                break;
            case 10:
                // maybe mowe?
                break;
            case 12:
                // ready to harvest?
                break;
            default:
                //nothing special happens
                break;
        }

    }
}
