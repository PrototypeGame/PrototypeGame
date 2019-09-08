using UnityEngine;
using System.Collections;

public class ArcherAttack : Attack
{
    public GameObject ball;

    public override void Skill_Auto()
    {
        if (Timer[0].notInCool)
        {
            GameTimer.TimerRemainResetToCool(Timer[0]);
            anim.SetFloat("speed", 0.0f);
            PlayerControl.isMovable = false;

            transform.rotation = Quaternion.LookRotation(ArrowControl.arrowDest);
            anim.SetInteger("skill", 0);
            Instantiate(ball, transform.position + Vector3.up, transform.rotation);
        }
    }

    public override void Skill_Slot_1()
    {
        if (Timer[1].notInCool)
        {
            GameTimer.TimerRemainResetToCool(Timer[1]);
            anim.SetFloat("speed", 0.0f);
            PlayerControl.isMovable = false;

            anim.SetInteger("skill", 1);
            Debug.Log("Skill 1 Used");
        }
    }

    public override void Skill_Slot_2()
    {
        if (Timer[2].notInCool)
        {
            GameTimer.TimerRemainResetToCool(Timer[2]);
            anim.SetFloat("speed", 0.0f);
            PlayerControl.isMovable = false;

            anim.SetInteger("skill", 2);
            Debug.Log("Skill 2 Used");
        }
    }

    public override void Skill_Slot_3()
    {
        
    }

    public override void Skill_Ultimate()
    {
        
    }
}
