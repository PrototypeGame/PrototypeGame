using UnityEngine;
using System.Collections;

public class WorriorAttack : Attack
{
    public override void Skill_Auto()
    {
        if (Timer[0].notInCool)
        {
            GameTimer.TimerRemainResetToCool(Timer[0]);
        }
    }
    public override void Skill_Slot_1() { }
    public override void Skill_Slot_2() { }
    public override void Skill_Slot_3() { }
    public override void Skill_Ultimate() { }
}
