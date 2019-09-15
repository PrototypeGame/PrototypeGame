using UnityEngine;
using System.Collections;

public class ArcherAttack : PlayerAttack
{
    public GameObject ball;

    public override void Skill_Auto()
    {
        if (timer[0].notInCool)
        {
            GameTimer.TimerRemainResetToCool(timer[0]);
            //anim.SetFloat("speed", 0.0f);
            PlayerControl.isMovable = false;

            transform.rotation = Quaternion.LookRotation(PlayerArrow.arrowDest);
            //anim.SetInteger("skill", 0);
            Instantiate(ball, transform.position + Vector3.up, transform.rotation);
        }
    }

    public override void Skill_Slot_1()
    {
        if (timer[1].notInCool)
        {
            GameTimer.TimerRemainResetToCool(timer[1]);
            //anim.SetFloat("speed", 0.0f);
            PlayerControl.isMovable = false;

            //anim.SetInteger("skill", 1);
            Debug.Log("Skill 1 Used");
        }
    }

    public override void Skill_Slot_2()
    {
        if (timer[2].notInCool)
        {
            GameTimer.TimerRemainResetToCool(timer[2]);
            //anim.SetFloat("speed", 0.0f);
            PlayerControl.isMovable = false;

            //anim.SetInteger("skill", 2);
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
