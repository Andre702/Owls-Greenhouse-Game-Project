using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hartleaf : Plant
{
    //Custom Needs:
    public bool needsToBeAlone = false;
    public bool needsToBeNearOther = false;

    private int[] waterAtHours = { 1, 10, 12 };
    private int aloneUntill = 5;
    private int nearOtherFrom = 12;
    private int matureAtAge = 14;
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
            case 5:
                needsToBeAlone = false;
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
                isHappy = true;
                break;
            default:
                //nothing special happens
                break;
        }

    }

    public override bool CheckNeeds()
    {
        if (stage >= 5 || plantName == PlantName.DEAD)
        {
            return true;
        }
        if (needsToBeAlone)
        {
            needsToBeAlone = !GameManager.instance.PlantNeedsCheck(this.index, PlantNeed.Alone); ;
        }
        if (needsToBeNearOther)
        {
            needsToBeNearOther = !GameManager.instance.PlantNeedsCheck(this.index, PlantNeed.NearOther); ;
        }
        if (needsWater)
        {
            return false;
        }
        if (needsToBeAlone || needsToBeNearOther)
        {
            return false;
        }
        
        return true;
    }

    protected override string Greeting()
    {
        return $"O... hi. I'm a Hartleaf. I grow int pot {index}";
    }

    protected override string Age()
    {
        if (this.plantAge >= matureAtAge)
        {
            return $"I am a matured Hartleaf. " +
                $"\nI'm currently {this.plantAge} hours old.";
        }
        else if (this.stage <= 3)
        {
            return $"My age? Well... I am {this.plantAge} hours old." +
                $"\n I... I hope I will bloom in about {matureAtAge - this.plantAge} hours.";
        }
        else
        {
            return $"My age? I am {this.plantAge} hours old." +
                $"\n I will achieve full bloom in {matureAtAge - this.plantAge} hours.";
        }
    }

    protected override string HowFeel()
    {
        if (this.isHappy)
        {
            if (this.plantHealth > 1)
            {
                return $"I'm feeling quite good right now. My health is: {this.plantHealth}.";
            }
            else
            {
                return $"I'm feeling happy right now, but also very weak. My health is only: {this.plantHealth}.";
            }
        }
        else
        {
            string answer = "";
            answer += $"I feel... sad right now. My health is: {this.plantHealth}, but more importantly I ";
            if (needsWater) 
            { 
                answer += "need water"; 
                if (needsToBeAlone || needsToBeNearOther) { answer += " and I "; }
            }
            if (needsToBeAlone) { answer += "want to be alone"; }
            if (needsToBeNearOther) { answer += "want to be near someone"; }

            answer += ".";
            return answer;
        }
    }

    protected override string Needs()
    {
        if (stage >= 5)
        {
            return "Thank You, but I don't need anything.\n I'm a mature plant now. I'm fine on my own.";
        }

        string answer = "";
        string waterHours = "";

        bool firstHour = false;
        foreach (int waterHour in waterAtHours)
        {
            if (waterHour > plantAge)
            {
                if (firstHour) { waterHours += " and "; }
                waterHours += $"{waterHour - plantAge}";
                firstHour = true;
            }
        }

        if (waterHours != "")
        {
            waterHours += " hours. ";
            answer += "I will need water in ";
            answer += waterHours;
        }

        if (aloneUntill > plantAge)
        {
            answer += $"\nI would like to be alone until age of {aloneUntill}. ";
        }

        if (nearOtherFrom > plantAge)
        {
            answer += $"\nI also need to be close to someone from age of {nearOtherFrom}. ";
        }

        if (answer == "")
        {
            return "Well... I may be still growing but I won't bee needing anything. Thanks for taking care of me.";
        }

        return answer;
    }
}
