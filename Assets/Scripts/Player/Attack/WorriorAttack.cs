using UnityEngine;
using System.Collections;

public class WorriorAttack : PlayerAttack
{
    public override void Skill_Auto()
    {
        if (manager.isTargeted)
        {
            if (DetectUtil.Detect(sight, manager.targetEnemy.hitCol))
            {
                if (timer[0].notInCool && !skillAutoOnGoing)
                {
                    control.isMovable = false;
                    skillAutoOnGoing = true;
                    TimerUtil.TimerRemainResetToCool(timer[0]);

                    // Enemy에게 Hit 판정 내리기
                    manager.anim.SetInteger("skill", 0);

                    manager.targetEnemy.hitCol.transform.root.gameObject.SetActive(false);

                    Debug.Log(manager.targetEnemy.hitCol.transform.root.name + "에게 공격 성공함");

                    skillAutoOnGoing = false;
                }
            }
        }
    }
    public override void Skill_Slot_1() { }
    public override void Skill_Slot_2() { }
    public override void Skill_Slot_3() { }
    public override void Skill_Ultimate() { }
}
