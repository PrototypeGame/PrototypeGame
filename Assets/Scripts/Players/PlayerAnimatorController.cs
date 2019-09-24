using UnityEngine;
using System.Collections;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator anim;

    #region Animator State Hash

    private readonly int hashState = Animator.StringToHash("state");

    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void DefaultPlayOnStateChange(PlayerableCharacterState animStat)
    {
        anim.SetInteger(hashState, (int)animStat);
    }
    public void SkillPlayOnStateChange(/* Skill Num */)
    {
        // Play Skill
    }
}