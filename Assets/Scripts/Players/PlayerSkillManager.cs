using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boss;

public class PlayerSkillManager : MonoBehaviour
{
    private PlayerFSMManager manager;

    public SkillData normalAttackData;

    public int normalSkillStack;
    public bool isLinkToNext;

    private void Awake()
    {
        manager = GetComponent<PlayerFSMManager>();

        normalAttackData = new SkillData();
    }

    public IEnumerator NormalAttack()
    {
        BossMonsterBase[] tempAttackBoss = BossUtil.GetBossComponents(DetectUtil.DetectObjectsTransformWithAngle(BossUtil.GetBossLocations(manager.boss), manager.transf, 60.0f, 10.0f));

        foreach (var item in tempAttackBoss)
        {
            // TODO: item에 데미지를 가한다
            // 데미지 : manager.statusManager.GetFinalAttackDamage();

            Debug.Log("데미지 적용");
            Debug.Log("데미지 : " + manager.statusManager.GetFinalAttackDamage());
        }

        yield return new WaitForEndOfFrame();
    }
}
