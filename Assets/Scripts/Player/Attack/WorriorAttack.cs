using UnityEngine;
using System.Collections;

public class WorriorAttack : PlayerAttack
{
    // 거리
    public float attackDistance;
    // 범위 (가로, 세로길이)
    public float attackLength;

    public override void Skill_Auto()
    {
        if (timer[0].notInCool)
        {
            GameTimer.TimerRemainResetToCool(timer[0]);

            foreach (var enemy in manager.enemys)
            {
                if (DetectUtil.Detect(manager.sight, attackDistance, attackLength, enemy.hitCol))
                {
                    // Enemy에게 Hit 판정 내리기

                    Debug.Log(enemy.transform.root.name + "에게 공격 성공함");
                }
            }
        }
    }
    public override void Skill_Slot_1() { }
    public override void Skill_Slot_2() { }
    public override void Skill_Slot_3() { }
    public override void Skill_Ultimate() { }
}
