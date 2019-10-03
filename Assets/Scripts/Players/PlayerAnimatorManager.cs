using UnityEngine;
using System.Collections;

public class PlayerAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator anim;

    #region Animator State Hash

    private readonly int hashState = Animator.StringToHash("state");

    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(PlayableCharacterState state)
    {
        anim.SetInteger(hashState, (int)state);
        Debug.Log("Current Int Anim : " + anim.GetInteger(hashState));
    }
}