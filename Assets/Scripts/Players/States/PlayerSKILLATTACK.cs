using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSKILLATTACK : PlayerFSMState
{
    private Action skillDelegate;

    private void OnEnable()
    {

    }

    public override void FSMStart()
    {
        base.FSMStart();
    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();


    }

    public override void FSMFixedUpdate()
    {
        base.FSMFixedUpdate();


    }

    public void SkillSelectRun(GameKeyPreset skillKey)
    {
        switch (PlayerInputController.ReturnInputKey(manager.skillKeys))
        {
            case GameKeyPreset.Skill_1:
                skillDelegate = Skill_1_Anim;
                break;
            case GameKeyPreset.Skill_2:
                skillDelegate = Skill_2_Anim;
                break;
            case GameKeyPreset.Skill_3:
                skillDelegate = Skill_3_Anim;
                break;
            case GameKeyPreset.Skill_Ultimate:
                skillDelegate = Skill_Ultimate_Anim;
                break;
            default:
                break;
        }

        skillDelegate?.Invoke();
    }

    private void Skill_1_Anim()
    {
        if (!TimerUtil.IsOnCoolTime(manager.timeManager.skillAttackTimers[0]))
        {
            TimerUtil.TimerReset(manager.timeManager.skillAttackTimers[0]);
            Debug.Log("Skill 1");

            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
        else
        {
            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
    }
    private void Skill_2_Anim()
    {
        if (!TimerUtil.IsOnCoolTime(manager.timeManager.skillAttackTimers[1]))
        {
            TimerUtil.TimerReset(manager.timeManager.skillAttackTimers[1]);
            Debug.Log("Skill 2");

            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
        else
        {
            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
    }
    private void Skill_3_Anim()
    {
        if (!TimerUtil.IsOnCoolTime(manager.timeManager.skillAttackTimers[2]))
        {
            TimerUtil.TimerReset(manager.timeManager.skillAttackTimers[2]);
            Debug.Log("Skill 3");

            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
        else
        {
            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
    }
    private void Skill_Ultimate_Anim()
    {
        if (!TimerUtil.IsOnCoolTime(manager.timeManager.skillAttackTimers[3]))
        {
            TimerUtil.TimerReset(manager.timeManager.skillAttackTimers[3]);
            Debug.Log("Skill Ultimate");

            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
        else
        {
            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
    }
}
