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

        if (TimerUtil.IsOnCoolTime(manager.timeManager.dashTimer))
        {
            Debug.Log("[DEBUG] Dash Cool is not Complete");
            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
        else
        {
            Debug.Log("[DEBUG] Dash Input detected");
            DashMove();

            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();
    }

    private void DashMove()
    {
        TimerUtil.TimerReset(manager.timeManager.dashTimer);
        MovementUtil.ForceDashMove(manager.rigid, manager.transf, Vector3.forward, dashPower, ForceMode.Impulse);
    }
}