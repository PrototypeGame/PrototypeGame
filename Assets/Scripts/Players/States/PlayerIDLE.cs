using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIDLE : PlayerFSMState
{
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
            Debug.Log("[DEBUG] Move Input detected");
            manager.SetPlayerState(PlayerableCharacterState.MOVE);
        }
        // Attack Check
        else if (PlayerInputController.CheckInputSignal(manager.attackKeys))
        {
            Debug.Log("[DEBUG] Attack Input detected");
            manager.SetPlayerState(PlayerableCharacterState.NORMALATTACK);
        }
    }

    public override void FSMFixedUpdate()
    {
        base.FSMFixedUpdate();


    }

    public override void FSMAnimationPlay()
    {
        base.FSMAnimationPlay();
    }
}
