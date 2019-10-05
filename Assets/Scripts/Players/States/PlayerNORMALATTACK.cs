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

        Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 스크립트 진입, 일반 공격 스택 : " + manager.animManager.normalAttackStack);

        StartCoroutine(NormalAttackAnim());
    }

    private IEnumerator InputForNextStack()
    {
        yield return new WaitForFixedUpdate();

        if (manager.animManager.canNormalAttackLinkCheck && !manager.animManager.isLinked)
        {
            Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 스택 링크 체크 가능");

            if (manager.animManager.normalAttackStack < 3)
            {
                yield return new WaitUntil(() => GameKey.GetKeyDown(GameKeyPreset.NormalAttack));

                manager.animManager.isLinked = true;
                Debug.LogError("Link State : " + manager.animManager.isLinked);
            }
        }
    }

    private IEnumerator NormalAttackAnim()
    {
        if (manager.animManager.normalAttackStack == 1)
        {
            if (!TimerUtil.IsOnCoolTime(manager.timeManager.normalAttackTimer))
            {
                Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 시작, 일반 공격 스택 : " + manager.animManager.normalAttackStack);
                manager.animManager.PlayAnimation(PlayableCharacterState.NORMALATTACK);
            }
            else FSMNextState();
        }
        else if (manager.animManager.normalAttackStack < 1)
        {
            Debug.LogError("잘못된 normalAttackStack 값 : " + manager.animManager.normalAttackStack);
            Debug.LogError("IDLE로 복귀");

            FSMNextState();
        }

        yield return new WaitUntil(() => manager.animManager.isEndNormalAttack);

        Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 끝남");

        //if (manager.animManager.normalAttackStack == 1)
        //{
        //    if (!TimerUtil.IsOnCoolTime(manager.timeManager.normalAttackTimer))
        //    {
        //        Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 시작, 일반 공격 스택 : " + manager.animManager.normalAttackStack);
        //
        //        manager.animManager.PlayAnimation(PlayableCharacterState.NORMALATTACK);
        //
        //        yield return new WaitUntil(() => manager.animManager.isEndNormalAttack);
        //
        //        Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 끝남");
        //    }
        //    else FSMNextState();
        //}
        //else if (manager.animManager.normalAttackStack > 1)
        //{
        //    Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 시작, 일반 공격 스택 : " + manager.animManager.normalAttackStack);
        //
        //    manager.animManager.PlayAnimation(PlayableCharacterState.NORMALATTACK);
        //
        //    yield return new WaitUntil(() => manager.animManager.isEndNormalAttack);
        //
        //    Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 끝남");
        //}
        //else
        //{
        //    Debug.LogError("잘못된 normalAttackStack 값 : " + manager.animManager.normalAttackStack);
        //}
    }

    public override void FSMNextState()
    {
        manager.SetPlayerState(PlayableCharacterState.IDLE);
    }
}
