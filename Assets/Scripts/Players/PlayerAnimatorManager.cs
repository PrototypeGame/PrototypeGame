using UnityEngine;
using System.Collections;

public class PlayerAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerFSMManager manager;

    #region Animator State Hash

    private readonly int hashState = Animator.StringToHash("state");
    private readonly int hashAttackLink = Animator.StringToHash("attackLink");

    #endregion

    public bool isLinked = false;

    #region Normal Attack Link Check

    public int normalAttackStack;

    public bool isEndNormalAttack = false;
    public bool canNormalAttackLinkCheck = false;

    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
        manager = GetComponentInParent<PlayerFSMManager>();

        normalAttackStack = 1;
    }

    public void PlayAnimation(PlayableCharacterState state)
    {
        anim.SetInteger(hashState, (int)state);
    }

    public void LinkNextNormalAttack()
    {
        anim.SetTrigger(hashAttackLink);
    }

    public void AllowCheckNormalAttackLink()
    {
        Debug.Log("[디버그][PlayerAnimatorManager.cs] 일반 공격 스택 링크 체크 가능 (Animation Event)");
        canNormalAttackLinkCheck = true;
    }

    public void EndNormalAttackStack()
    {
        isEndNormalAttack = true;

        canNormalAttackLinkCheck = false;
    }
}