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


    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();

        // Move Check
        if (PlayerInputController.CheckInputSignal(manager.moveKeys))
        {
            //Debug.Log("[DEBUG] Move Input detected");
            manager.SetPlayerState(PlayableCharacterState.MOVE);
        }
        else if (GameKey.GetKeyDown(GameKeyPreset.Dash))
        {
           // Debug.Log("[DEBUG] Dash Input detected");
            manager.SetPlayerState(PlayableCharacterState.DASH);
        }
        // Attack Check
        else if (PlayerInputController.CheckInputSignal(manager.attackKeys))
        {
           // Debug.Log("[DEBUG] Attack Input detected");
            manager.SetPlayerState(PlayableCharacterState.NORMALATTACK);
        }
        else if (PlayerInputController.CheckInputSignal(manager.skillKeys))
        {
           // Debug.Log("[DEBUG] Skill Input detected");
            manager.SetPlayerState(PlayableCharacterState.SKILLATTACK);
            ((manager.currentFSMAction) as PlayerSKILLATTACK).SkillSelectRun(PlayerInputController.ReturnInputKey(manager.skillKeys));
        }
    }

    public override void FSMFixedUpdate()
    {
        base.FSMFixedUpdate();


    }
}
