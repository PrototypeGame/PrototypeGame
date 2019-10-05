using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIDLE : PlayerFSMState
{
    private void OnEnable()
    {
        
    }

    public override void FSMStart()
    {
        base.FSMStart();

        manager.animManager.PlayAnimation(PlayableCharacterState.IDLE);
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();

        FSMNextState();
    }

    public override void FSMNextState()
    {
        // Move Check
        if (InputControlUtil.CheckInputSignal(manager.inputManager.moveKeys))
        {
            manager.SetPlayerState(PlayableCharacterState.MOVE);
        }
        else if (GameKey.GetKeyDown(GameKeyPreset.Dash))
        {
            manager.SetPlayerState(PlayableCharacterState.DASH);
        }
        // Attack Check
        else if (InputControlUtil.CheckInputSignal(manager.inputManager.attackKeys))
        {
            manager.SetPlayerState(PlayableCharacterState.NORMALATTACK);
        }
        else if (InputControlUtil.CheckInputSignal(manager.inputManager.skillKeys))
        {
            manager.SetPlayerState(PlayableCharacterState.SKILLATTACK);
            ((manager.currentFSMAction) as PlayerSKILLATTACK).SkillSelectRun(InputControlUtil.ReturnInputKey(manager.inputManager.skillKeys));
        }
    }
}
