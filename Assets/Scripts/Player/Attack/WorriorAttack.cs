﻿using UnityEngine;
using System.Collections;

public class WorriorAttack : PlayerAttack
{
    public override void Skill_Auto_Anim()
    {
        if (manager.targetEnemy != null)
        {
            if (DetectUtil.AABBDetect(sight, manager.targetEnemy.hitCol))
            {
                if (timer[0].notInCool)
                {
                    control.isMovable = false;

                    manager.anim.SetInteger("skill", 0);

                    // 여기부터 추후 Main에 삽입
                    TimerUtil.TimerRemainResetToCool(timer[0]);

                    // Enemy에게 Hit 판정 내리기
                    manager.targetEnemy.hitCol.transform.root.gameObject.SetActive(false);

                    Debug.Log(manager.targetEnemy.hitCol.transform.root.name + "에게 공격 성공함");

                    control.pointer.SetState(PointState.DISABLE);

                    manager.TargetDestroy();
                }
            }
        }
    }
    public override void Skill_Auto_Main() { }
    public override void Skill_1_Anim() { }
    public override void Skill_1_Main() { }
    public override void Skill_2_Anim() { }
    public override void Skill_2_Main() { }
    public override void Skill_3_Anim() { }
    public override void Skill_3_Main() { }
    public override void Skill_Ultimate_Anim() { }
    public override void Skill_Ultimate_Main() { }
}
