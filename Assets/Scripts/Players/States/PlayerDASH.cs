using UnityEngine;
using System.Collections;

public class PlayerDASH : PlayerFSMState
{
    public float dashPower = 10.0f;

    private void OnEnable()
    {

    }

    public override void FSMStart()
    {
        base.FSMStart();

        Debug.Log("[DEBUG] Dash Input detected");
        DashMove();
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();
    }

    private void DashMove()
    {
        TimerUtil.TimerReset(manager.timeManager.dashTimer);
        MovementUtil.ForceDashMove(manager.rigid, manager.transf, Vector3.forward, dashPower, ForceMode.Impulse);

        if (InputControlUtil.CheckInputSignal(manager.inputManager.moveKeys))
        {
            manager.SetPlayerState(PlayableCharacterState.MOVE);
        }
        else
        {
            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
    }
}