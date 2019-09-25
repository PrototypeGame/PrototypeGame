using UnityEngine;
using System.Collections;

public class PlayerTimerManager : MonoBehaviour
{
    public TimerUtil normalAttackTimer;
    public TimerUtil[] skillAttackTimers;

    private void Awake()
    {
        skillAttackTimers = new TimerUtil[4];
    }

    public void TimerUpdate()
    {
        // Normal Attack Delay Timer
        TimerUtil.TimerOnGoing(normalAttackTimer);
        // Skill Cool Timer
        foreach (var item in skillAttackTimers)
        {
            TimerUtil.TimerOnGoing(item);
        }
    }
}
