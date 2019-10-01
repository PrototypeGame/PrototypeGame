using UnityEngine;
using System.Collections;

public class PlayerSkillManager : MonoBehaviour
{
    [HideInInspector]
    public SkillActivator activatedSkill = null;
    public int activatedSkillIndex = -1;
    public string activatedSkillName = null;

    public NormalSkill normalSkill;

    PlayerSkillManager()
    {
        normalSkill = new NormalSkill();
    }
}
