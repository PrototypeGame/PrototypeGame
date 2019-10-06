using UnityEngine;
using System.Collections;

public class PlayerDASH : PlayerFSMState
{
    public float dashPower = 10.0f;

    private void OnEnable()
    {
        StartCoroutine(DashMove());
    }

    private IEnumerator DashMove()
    {
        if (!TimerUtil.IsOnCoolTime(manager.timeManager.dashTimer))
        {
            TimerUtil.TimerReset(manager.timeManager.dashTimer);

            MovementUtil.ForceDashMove(manager.rigid, manager.transf, Vector3.forward, dashPower, ForceMode.Impulse);
            manager.animManager.PlayStateAnim(PlayableCharacterState.DASH);

            yield return new WaitWhile(() => manager.animManager.isAnimating);
        }

        FSMNextState();
    }

    public override void FSMNextState()
    {
        if (GameKey.GetKeys(GameKey.moveKeys))
            manager.SetPlayerState(PlayableCharacterState.MOVE);
        else
            manager.SetPlayerState(PlayableCharacterState.IDLE);
    }
}