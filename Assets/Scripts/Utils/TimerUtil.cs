using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimerUtil
{
    public static void TimerOnGoing(TimerUtil timer)
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

    public static void TimerRemainResetToCool(TimerUtil timer)
    {
        timer.remainTime = timer.timeCool;
        timer.notInCool = false;
    }
}
