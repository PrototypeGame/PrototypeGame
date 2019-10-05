using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSKILLATTACK : PlayerFSMState
{
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
        switch (InputControlUtil.ReturnInputKey(manager.inputManager.skillKeys))
        {
            case GameKeyPreset.Skill_1:
                manager.skillManager.activatedSkill = manager.skillManager.normalSkill;
                break;
            case GameKeyPreset.Skill_2:
                manager.skillManager.activatedSkill = null;
                break;
            case GameKeyPreset.Skill_3:
                manager.skillManager.activatedSkill = null;
                break;
            case GameKeyPreset.Skill_Ultimate:
                manager.skillManager.activatedSkill = null;
                break;
            default:
                manager.skillManager.activatedSkill = null;
                break;
        }

        if (manager.skillManager.activatedSkill != null)
        {
            manager.skillManager.activatedSkillIndex = manager.skillManager.activatedSkill.applyData.SkillIndex;
            manager.skillManager.activatedSkillName = manager.skillManager.activatedSkill.applyData.SkillName;

            SkillPlay();
        }
        else
        {
            manager.skillManager.activatedSkillIndex = -1;
            manager.skillManager.activatedSkillName = "NONE";
        }
    }

    public void SkillPlay()
    {
        if (!TimerUtil.IsOnCoolTime(manager.timeManager.skillAttackTimers[manager.skillManager.activatedSkillIndex]))
        {
            manager.skillManager.activatedSkill?.PlaySkillAnimation();
        }
    }

    public override void FSMNextState()
    {

    }
}
