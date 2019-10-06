using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorManager : MonoBehaviour
{
    private PlayerFSMManager manager;

    public Animator anim;

    private WaitForEndOfFrame waitAnimFrame;

    #region Animator State Hash

    public readonly int hashState = Animator.StringToHash("state");
    public readonly int hashAttackLink = Animator.StringToHash("attackLink");

    #endregion

    public bool isAnimating = false;

    private void Awake()
    {
        manager = GetComponentInParent<PlayerFSMManager>();

        anim = GetComponent<Animator>();

        waitAnimFrame = new WaitForEndOfFrame();
    }

    public void PlayStateAnim(PlayableCharacterState enumState)
    {
        StartCoroutine(AnimCoroutine(hashState, (int)enumState));
    }
    public void PlayLinkAnim()
    {
        StartCoroutine(AnimCoroutine(hashAttackLink));
    }

    #region Animation Coroutine

    private IEnumerator AnimCoroutine(int hash)
    {
        anim.SetTrigger(hash);
        yield return waitAnimFrame;
    }
    private IEnumerator AnimCoroutine(int hash, bool isEnable)
    {
        anim.SetTrigger(hash);
        yield return waitAnimFrame;
    }
    private IEnumerator AnimCoroutine(int hash, int value)
    {
        anim.SetInteger(hash, value);
        yield return waitAnimFrame;
    }
    private IEnumerator AnimCoroutine(int hash, float value)
    {
        anim.SetFloat(hash, value);
        yield return waitAnimFrame;
    }

    #endregion

    public void StartAnimationSection()
    {
        Debug.Log("[" + GetType().Name + "] " + "Animation Start");
        isAnimating = true;
    }

    public void EndAnimationSection()
    {
        Debug.Log("[" + GetType().Name + "] " + "Animation End");
        isAnimating = false;
    }

    public void ApplyNormalAttackInAnim()
    {
        Debug.Log("공격 적용 In 애니메이션");
        StartCoroutine(manager.skillManager.NormalAttack());
    }
}