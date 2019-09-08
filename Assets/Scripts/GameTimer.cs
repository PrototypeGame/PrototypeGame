using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameTimer
{
    //[HideInInspector]
    public bool notInCool;

    public float timeCool;
    //[HideInInspector]
    public float remainTime;

    GameTimer()
    {
        notInCool = true;
        remainTime = 0.0f;
    }

    public static void TimerOnGoing(GameTimer timer)
    {
        if (timer.remainTime > 0)
        {
            timer.remainTime -= Time.deltaTime;
        }
        else
        {
            timer.remainTime = 0.0f;
            timer.notInCool = true;
        }
    }

    public static void TimerRemainResetToCool(GameTimer timer)
    {
        timer.remainTime = timer.timeCool;
        timer.notInCool = false;
    }
}
