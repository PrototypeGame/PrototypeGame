using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNORMALATTACK : PlayerFSMState
{
    Coroutine playAnimRoutine;
    Coroutine inputKeyRoutine;

    private void OnDisable()
    {
        Debug.Log("[" + GetType().Name + "] " + "Script Disabled");
        Debug.Log("[" + GetType().Name + "] " + "---------------------------------------------------------------------");
    }

    public override void FSMStart()
    {
        playAnimRoutine = StartCoroutine(PlayAnimation());
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();

        if (manager.skillManager.normalSkillStack < 2)
        {
            if (manager.animManager.isAnimating)
            {
                if (GameKey.GetKeyDown(GameKeyPreset.NormalAttack))
                {
                    Debug.Log("[" + GetType().Name + "] " + "Key Inputed");
                    manager.skillManager.isLinkToNext = true;
                    Debug.Log("[" + GetType().Name + "] " + "Skill Linked : " + manager.skillManager.isLinkToNext);
                }
            }
        }
    }

    private IEnumerator PlayAnimation()
    {
        Debug.Log("[" + GetType().Name + "] " + "PlayAnimation() Enabled");
        Debug.Log("[" + GetType().Name + "] " + "Skill Stack Num : " + manager.skillManager.normalSkillStack);

        // 처음 스킬 애니메이션 실행
        if (manager.skillManager.normalSkillStack == 0)
        {
            manager.animManager.PlayStateAnim(PlayableCharacterState.NORMALATTACK);
        }
        // 두번째 스킬 애니메이션 실행
        else if (manager.skillManager.normalSkillStack > 0)
        {
            manager.animManager.PlayLinkAnim();
        }

        Debug.Log("[" + GetType().Name + "] " + "manager.animManager.isAnimating : " + manager.animManager.isAnimating + " / Animation Start Waiting");

        yield return new WaitUntil(() => manager.animManager.isAnimating);
        Debug.Log("[" + GetType().Name + "] " + "manager.animManager.isAnimating : " + manager.animManager.isAnimating + " / Animation Playing");

        yield return new WaitWhile(() => manager.animManager.isAnimating);
        Debug.Log("[" + GetType().Name + "] " + "manager.animManager.isAnimating : " + manager.animManager.isAnimating + " / End Animation");

        // 마지막 스킬 애니메이션 이전에 링크가 된 경우
        if (manager.skillManager.isLinkToNext && (manager.skillManager.normalSkillStack < 2))
        {
            manager.skillManager.normalSkillStack++;
            manager.skillManager.isLinkToNext = false;
            Debug.Log("[" + GetType().Name + "] " + "manager.skillManager.skillStack : " + manager.skillManager.normalSkillStack + " / Play Next Animation");

            Debug.Log("[" + GetType().Name + "] " + "Key Input Disabled");

            StartCoroutine(InitNormalAttack());
        }
        // 링크가 안되거나 마지막 스킬 이벤트인 경우
        else
        {
            Debug.Log("[" + GetType().Name + "] " + "manager.skillManager.skillStack : " + manager.skillManager.normalSkillStack + " / End Animation");

            manager.skillManager.normalSkillStack = 0;
            manager.skillManager.isLinkToNext = false;
            Debug.Log("[" + GetType().Name + "] " + "Initialized / skillStack : " + manager.skillManager.normalSkillStack + " & isLinkToNext : " + manager.skillManager.isLinkToNext);

            Debug.Log("[" + GetType().Name + "] " + "Go To IDLE");
            FSMNextState();
        }
    }

    private IEnumerator InitNormalAttack()
    {
        StopCoroutine(playAnimRoutine);

        yield return new WaitForEndOfFrame();

        playAnimRoutine = StartCoroutine(PlayAnimation());

        yield break;
    }

    public override void FSMNextState()
    {
        manager.SetPlayerState(PlayableCharacterState.IDLE);
    }
}
