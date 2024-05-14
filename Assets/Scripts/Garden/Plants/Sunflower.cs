using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : Plant
{
    private int[] waterAtHours = { 4, 8 };
    private int matureAtAge = 10;

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
                needsWater = true;
                break;
            case 6:
                stage += 1;
                break;
            case 8:
                stage += 1;
                needsWater = true;
                break;
            case 10:
                stage += 1;
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
        if (needsWater)
        {
            return false;
        }

        return true;
    }


    protected override string Greeting()
    {
        return $"Hello! I'm a Sunflower. I grow int pot {index}";
    }

    protected override string Age()
    {
        if (this.plantAge >= matureAtAge)
        {
            return $"I am a fully grown Sunflower! \nI'm currently {this.plantAge} hours old.";
        }
        else
        {
            return $"I am currently {this.plantAge} hours old.\n I can't wait to be fully grown! I will bloom in {matureAtAge - this.plantAge} hours.";
        }
    }

    protected override string HowFeel()
    {
        if (this.isHappy)
        {
            if (this.plantHealth == 4)
            {
                return $"I'm feeling super good! I'm happy and my health is full and is exactly: {this.plantHealth}.";
            }
            else if (plantHealth > 1)
            {
                return $"I'm feeling good! I'm happy and my health is: {this.plantHealth}.";
            }
            else
            {
                return $"I'm feeling happy, but I also feel pretty weak right now. My health is only: {this.plantHealth}.";
            }
        }
        else
        {
            string answer = "";
            answer += $"I feel a bit... under the weather If you know what I mean.\n" +
                $"My health is: {this.plantHealth} and ";
            if (needsWater)
            {
                answer += "I need some water.";
            }

            return answer;
        }
    }

    protected override string Needs()
    {
        if (stage >= 5)
        {
            return "I'm a fully grown and independent Sunflower now!" +
                "\n Don't worry about me, I'm all good and happy from now on.";
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
            answer += "I will love to get some water in ";
            answer += waterHours;

            if (!isHappy)
            {
                answer += "I also... feel a bit dizzy right now.";
            }
        }

        if (answer == "")
        {
            if (isHappy)
            {
                return "I may be still growing but I will be fine from now on." +
                    "\n Just give me some time and I will grow tall and strong!";
            }
            return "I need one more wattering right now and I should be good to go." +
                "\n Just give me some time and I will grow tall and strong!";


        }

        return answer;
    }
}
