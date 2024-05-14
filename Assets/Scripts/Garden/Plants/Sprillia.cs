using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprillia : Plant
{

    private int[] waterAtHours = { 10, 11 };
    private int matureAtAge = 13;

    public Sprillia(int _index) : base(_index)
    {
        plantName = PlantName.Sprillia;
        plantAge = 0;
        plantHealth = 2;
        isHappy = true;
        stage = 0;
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
                break;
            case 10:
                stage += 1;
                needsWater = true;
                break;
            case 11:
                needsWater = true;
                break;
            case 13:
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
        string answer = $"Greetings. I'm a plant from Sprillia fammilly. I grow int pot {index}";
        if (stage >= 4)
        {
            answer += "\nAren't my flowers splendid?";
        }
        return answer;
    }

    protected override string Age()
    {
        if (this.plantAge >= matureAtAge)
        {
            return $"I am a beautiful, matured Sprillia. \nI'm currently {this.plantAge} hours old.";
        }
        else
        {
            return $"How rude of you to ask a lady her age! " +
                $"\nWell If You need to know then I am {this.plantAge} hours old and shall bloom in {matureAtAge - this.plantAge} hours.";
        }
    }

    protected override string HowFeel()
    {
        if (this.isHappy)
        {
            if (this.plantHealth == 2)
            {
                return $"I'm feeling fine thank You." +
                    $"\nHowever I will have You know that I'm a delicate plant. My health is currently: {this.plantHealth}.";
            }
            else // HP == 1
            {
                return $"I'm fine, however my health is only: {this.plantHealth}." +
                    $"\nHow shamefull of you to not tend to my needs!";
            }
        }
        else
        {
            string answer = "";
            answer += "I feel terrible! I";
            if (needsWater)
            {
                answer += "need water immedietally to keep my beautiful flowers in perfect condition!";
            }

            return answer;
        }
    }

    protected override string Needs()
    {
        if (stage >= 5)
        {
            return "How nice of You to still tend to my needs even though I am a matured plant. Your service shall not be forgotten";
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
            if (!isHappy)
            {
                return "How about You tend to my current needs before you ask what would be the next one!?";
            }

            waterHours += " hours. ";
            answer += "I will require water for my future beauty in exactly ";
            answer += waterHours;
        }

        if (answer == "")
        {
            if (isHappy)
            {
                return "I won't need Your help anymore. My flowers shall bloom soon! Gosh I can't wait.";
            }

            return "I won't need anything later, but I want something NOW!" +
                "\nYou would know If you paid more attention to me!.";
        }

        return answer;
    }
}
