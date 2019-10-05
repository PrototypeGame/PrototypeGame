using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNORMALATTACK : PlayerFSMState
{
    public override void FSMStart()
    {
        base.FSMStart();

        manager.animManager.isEndNormalAttack = false;
        manager.animManager.isLinked = false;

        Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 스크립트 진입, 일반 공격 스택 : " + manager.animManager.normalAttackStack);

        StartCoroutine(NormalAttackAnim());
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();

        if (manager.animManager.canNormalAttackLinkCheck)
        {
            if (manager.animManager.normalAttackStack <= 3)
            {
                if (InputControlUtil.CheckInputSignal(manager.inputManager.attackKeys))
                    manager.animManager.isLinked = true;
                else
                    manager.animManager.isLinked = false;
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
                yield return new WaitUntil(() => manager.animManager.isEndNormalAttack);

                Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 끝남");

                CheckIfNormalAttackLinked();
            }
            else FSMNextState();
        }
        else if (manager.animManager.normalAttackStack == 2)
        {
            Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 시작, 일반 공격 스택 : " + manager.animManager.normalAttackStack);

            manager.animManager.PlayAnimation(PlayableCharacterState.NORMALATTACK);
            yield return new WaitUntil(() => manager.animManager.isEndNormalAttack);

            Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 끝남");

            CheckIfNormalAttackLinked();
        }
        else if (manager.animManager.normalAttackStack == 3)
        {
            Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 시작, 일반 공격 스택 : " + manager.animManager.normalAttackStack);

            manager.animManager.normalAttackStack = 1;

            manager.animManager.PlayAnimation(PlayableCharacterState.NORMALATTACK);
            yield return new WaitUntil(() => manager.animManager.isEndNormalAttack);

            Debug.Log("[디버그][PlayerNORMALATTACK.cs] 일반 공격 마지막 애니메이션 끝남");

            FSMNextState();
        }
        else
        {
            Debug.LogError("manager.animManager.normalAttackStack 가 올바르지 않은 값을 보유함 : " + manager.animManager.normalAttackStack);
        }
    }

    public void CheckIfNormalAttackLinked()
    {
        if (manager.animManager.isLinked)
        {
            Debug.Log("[디버그][PlayerAnimatorManager.cs] 일반 공격 링크됨");

            manager.animManager.normalAttackStack++;
            manager.animManager.LinkNextNormalAttack();
        }
        else
        {
            manager.animManager.normalAttackStack = 1;
            TimerUtil.TimerReset(manager.timeManager.normalAttackTimer);

            Debug.Log("[디버그][PlayerAnimatorManager.cs] 일반 공격 쿨타임 리셋됨, 일반 공격 스택 : " + manager.animManager.normalAttackStack);

            FSMNextState();
        }
    }

    public override void FSMNextState()
    {
        base.FSMNextState();

        manager.SetPlayerState(PlayableCharacterState.IDLE);
    }
}
