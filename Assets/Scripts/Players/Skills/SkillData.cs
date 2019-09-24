using UnityEngine;
using System.Collections;

public enum SkillType { ACTIVE, PASSIVE }
public enum SkillRare { NORMAL, ULTIMATE }
public enum SkillTarget { PLAYER_OWN = 1, PLAYER_OTHER = 2, ENEMY = 4, ONLY_BOSS = 8 }

public class SkillData
{
    [SerializeField]
    private int skillIndex;
    [SerializeField]
    private string skillName;
    // 스킬 사용 직업
    [SerializeField]
    private SkillRare skillRare;
    [SerializeField]
    private int skillSlot;
    [SerializeField]
    private SkillType skillType;
    [SerializeField]
    private SkillTarget skillApplyTarget;
    [SerializeField]
    private float skillApplyRange;
}
