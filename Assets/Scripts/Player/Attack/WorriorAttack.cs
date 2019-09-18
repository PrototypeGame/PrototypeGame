using UnityEngine;
using System.Collections;

public class WorriorAttack : PlayerAttack
{
    // 거리
    public float attackDistance;
    // 범위 (가로, 세로길이)
    public float attackLength;

    public override IEnumerator Skill_Auto()
    {
        foreach (var enemy in manager.enemys)
        {
            if (DetectUtil.Detect(manager.sight, attackDistance, attackLength, enemy.hitCol))
            {
                // Enemy에게 Hit 판정 내리기

                yield return new WaitUntil(() => animSender.tAttackAllow);

                enemy.transform.root.gameObject.SetActive(false);
    
                Debug.Log(enemy.transform.root.name + "에게 공격 성공함");
            }
        }

        yield break;
    }
    //public override IEnumerator Skill_Slot_1() { }
    //public override IEnumerator Skill_Slot_2() { }
    //public override IEnumerator Skill_Slot_3() { }
    //public override IEnumerator Skill_Ultimate() { }
}
