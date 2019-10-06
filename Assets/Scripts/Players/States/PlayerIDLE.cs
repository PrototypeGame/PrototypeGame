using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIDLE : PlayerFSMState
{
    private void OnEnable()
    {
        manager.animManager.PlayStateAnim(PlayableCharacterState.IDLE);
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();

        FSMNextState();
    }

    public override void FSMNextState()
    {
        if (GameKey.GetKeyDown(GameKeyPreset.NormalAttack))
        {
            manager.SetPlayerState(PlayableCharacterState.NORMALATTACK);
        }
        if (GameKey.GetKeysDown(GameKey.skillKeys))
        {
            manager.SetPlayerState(PlayableCharacterState.SKILLATTACK);
        }
        if (GameKey.GetKeys(GameKey.moveKeys))
        {
            manager.SetPlayerState(PlayableCharacterState.MOVE);
        }
        if (GameKey.GetKeyDown(GameKeyPreset.Dash))
        {
            if (!TimerUtil.IsOnCoolTime(manager.timeManager.dashTimer))
                manager.SetPlayerState(PlayableCharacterState.DASH);
        }
    }
}
