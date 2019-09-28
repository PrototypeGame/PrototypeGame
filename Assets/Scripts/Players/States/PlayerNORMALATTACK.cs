using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNORMALATTACK : PlayerFSMState
{
    private void OnEnable()
    {

    }

    public override void FSMStart()
    {
        base.FSMStart();
        
        if (!TimerUtil.IsOnCoolTime(manager.timeManager.normalAttackTimer))
        {
            TimerUtil.TimerReset(manager.timeManager.normalAttackTimer);
            // TODO: 에니메이션 공격 재생 버그 수정
            Debug.Log("[DEBUG] Normal Attack Animation Played");
            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
        // Cooltime is not over, return to IDLE
        else
        {
            Debug.Log("[DEBUG] Cool is not over");
            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();


    }

    public override void FSMFixedUpdate()
    {
        base.FSMFixedUpdate();


    }
}
