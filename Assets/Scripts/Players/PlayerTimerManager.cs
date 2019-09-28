using UnityEngine;
using System.Collections;

public class PlayerTimerManager : MonoBehaviour
{
    public GameTimer normalAttackTimer;
    public GameTimer[] skillAttackTimers;

    public GameTimer dashTimer;

    public GameTimer[] itemUseTimer;

    PlayerTimerManager()
    {
        skillAttackTimers = new GameTimer[4];
        itemUseTimer = new GameTimer[6];
    }

    private void Awake()
    {
        normalAttackTimer.coolTime = 1.0f;

        skillAttackTimers[0].coolTime = 1.0f;
        skillAttackTimers[1].coolTime = 1.0f;
        skillAttackTimers[2].coolTime = 1.0f;
        skillAttackTimers[3].coolTime = 1.0f;

        dashTimer.coolTime = 2.0f;

        // TODO: 추후에 ITEM MANAGE CLASS에서 정보 불러오기
        itemUseTimer[0].coolTime = 1.0f;
        itemUseTimer[1].coolTime = 1.0f;
        itemUseTimer[2].coolTime = 1.0f;
        itemUseTimer[3].coolTime = 1.0f;
        itemUseTimer[4].coolTime = 1.0f;
        itemUseTimer[5].coolTime = 1.0f;
    }

    public void TimerUpdate()
    {
        // Normal Attack Delay Timer
        TimerUtil.TimerCyclePlay(normalAttackTimer);
        // Skill Cool Timer
        if (skillAttackTimers.Length > 0)
        {
            foreach (var item in skillAttackTimers)
            {
                TimerUtil.TimerCyclePlay(item);
            }
        }

        // Dash Timer
        TimerUtil.TimerCyclePlay(dashTimer);

        // Item Use Timer
        if (itemUseTimer.Length > 0)
        {
            foreach (var item in itemUseTimer)
            {
                TimerUtil.TimerCyclePlay(item);
            }
        }
    }
}
