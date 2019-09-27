using UnityEngine;
using System.Collections;

public class PlayerTimerManager : MonoBehaviour
{
    public TimerUtil normalAttackTimer;
    public TimerUtil[] skillAttackTimers;

    private void Awake()
    {
        normalAttackTimer = new TimerUtil();
        skillAttackTimers = new TimerUtil[4];
    }

    public void TimerUpdate()
    {
        // Normal Attack Delay Timer
        TimerUtil.TimerOnGoing(normalAttackTimer);
        // Skill Cool Timer
        if (skillAttackTimers.Length > 0)
        {
            foreach (var item in skillAttackTimers)
            {
                TimerUtil.TimerOnGoing(item);
            }
        }
    }
}
