using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNORMALATTACK : PlayerFSMState
{
    public override void FSMStart()
    {
        base.FSMStart();
        
        if (manager.timeManager.normalAttackTimer.notInCool)
        {
            // TODO: 에니메이션 공격 재생 버그 수정
            Debug.Log("[DEBUG] Normal Attack Animation Played");
            base.FSMAnimationPlay();
            manager.animCtrl.DefaultPlayOnStateChange(PlayerableCharacterState.IDLE);
        }
        // Cooltime is not over, return to IDLE
        else
        {
            Debug.Log("[DEBUG] Cool is not over");
            manager.SetPlayerState(PlayerableCharacterState.IDLE);
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
