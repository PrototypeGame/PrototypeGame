using UnityEngine;
using System.Collections;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator anim;

    private PlayerFSMManager manager;

    #region Animator State Hash

    private readonly int hashState = Animator.StringToHash("state");

    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();

        manager = GetComponentInParent<PlayerFSMManager>();
    }

    // TODO: 에니메이션 공격 재생 버그 수정
    private void Update()
    {
        UpdateAnimation();
    }

    public void PlayStateAnimation(PlayableCharacterState animStat)
    {
        anim.SetInteger(hashState, (int)animStat);
    }
    public void SkillPlayOnStateChange(/* Skill Num */)
    {
        // Play Skill
    }

    private void UpdateAnimation()
    {
        if (manager.currentState == PlayableCharacterState.IDLE)
        {
            PlayStateAnimation(PlayableCharacterState.IDLE);
        }
        else if (manager.currentState == PlayableCharacterState.MOVE)
        {
            PlayStateAnimation(PlayableCharacterState.MOVE);
        }
        else if (manager.currentState == PlayableCharacterState.NORMALATTACK)
        {
            PlayStateAnimation(PlayableCharacterState.NORMALATTACK);
        }
        else if (manager.currentState == PlayableCharacterState.SKILLATTACK)
        {

        }
        else if (manager.currentState == PlayableCharacterState.DAMAGE)
        {

        }
        else if (manager.currentState == PlayableCharacterState.DEAD)
        {

        }
    }
}