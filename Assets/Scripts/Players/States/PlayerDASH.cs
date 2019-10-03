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
        StartCoroutine(DashMove());
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();
    }

    private IEnumerator DashMove()
    {
        if (!TimerUtil.IsOnCoolTime(manager.timeManager.dashTimer))
        {
            TimerUtil.TimerReset(manager.timeManager.dashTimer);
            MovementUtil.ForceDashMove(manager.rigid, manager.transf, Vector3.forward, dashPower, ForceMode.Impulse);

            manager.animManager.PlayAnimation(PlayableCharacterState.DASH);

            yield return new WaitForEndOfFrame();
        }

        FSMNextState();

        yield return null;
    }

    public override void FSMNextState()
    {
        base.FSMNextState();

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