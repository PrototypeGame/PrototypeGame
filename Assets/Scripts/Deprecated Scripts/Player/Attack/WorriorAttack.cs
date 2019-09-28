using UnityEngine;
using System.Collections;

public class WorriorAttack : PlayerAttack
{
    public override void Skill_Auto_Anim()
    {
        if (manager.targetEnemy != null)
        {
            if (DetectUtil.AABBDetect(sight, manager.targetEnemy.hitCol) && !TimerUtil.IsOnCoolTime(timer[0]))
            {
                TimerUtil.TimerReset(timer[0]);

                control.MoveStop();
                manager.anim.SetInteger("skill", 0);
            }
            else
            {
                control.MoveRestart();
                manager.anim.SetInteger("skill", -1);
            }
        }
        else
        {
            manager.anim.SetInteger("skill", -1);
        }
    }
    public override void Skill_Auto_Main()
    {
        // Enemy에게 Hit 판정 내리기
        manager.targetEnemy.hp -= 10.0f;
        Debug.Log("Current Enemy HP : " + manager.targetEnemy.hp);

        if (manager.targetEnemy.hp <= 0.0f)
        {
            manager.targetEnemy.hitCol.transform.root.gameObject.SetActive(false);

            Debug.Log(manager.targetEnemy.hitCol.transform.root.name + "에게 공격 성공함");

            manager.TargetDestroy();
            manager.anim.SetInteger("skill", -1);
        }
    }
    public override void Skill_1_Anim() { }
    public override void Skill_1_Main() { }
    public override void Skill_2_Anim() { }
    public override void Skill_2_Main() { }
    public override void Skill_3_Anim() { }
    public override void Skill_3_Main() { }
    public override void Skill_Ultimate_Anim() { }
    public override void Skill_Ultimate_Main() { }
}
