using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class SkillActivator : IPlayerSkillMethod
{
    public SkillData applyData;

    public SkillActivator()
    {
        applyData = new SkillData();
    }

    public virtual void InitSkillStat() { }

    public virtual void ApplySkillDamage() { }
    public virtual void EndSkillTiming() { }
    public virtual void PlaySkillAnimation() { }
}
