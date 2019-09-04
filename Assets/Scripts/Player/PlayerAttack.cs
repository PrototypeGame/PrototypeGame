using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoClass;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    public GameTimer normalAttack;
    public GameTimer skill_1;
    public GameTimer skill_2;

    public GameObject ball;

    private bool usingSkill;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        usingSkill = false;
    }

    void Update()
    {
        KeyInput();

        GameTimer.TimerOnGoing(normalAttack);
        GameTimer.TimerOnGoing(skill_1);
        GameTimer.TimerOnGoing(skill_2);
    }

    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Skill_1();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Skill_2();
        }
    }

    public bool NormalAttack(Vector3 destLineVec)
    {
        if (normalAttack.notInCool)
        {
            GameTimer.TimerRemainResetToCool(normalAttack);

            anim.SetInteger("skill", 0);
            Instantiate(ball, transform.position + Vector3.up, Quaternion.LookRotation(destLineVec));

            return true;
        }
        else
            return false;
    }

    public void Skill_1()
    {
        if (skill_1.notInCool == true)
        {
            GameTimer.TimerRemainResetToCool(skill_1);

            anim.SetInteger("skill", 1);
            Debug.Log("Skill 1 Used");
        }
    }

    public void Skill_2()
    {
        if (skill_2.notInCool == true)
        {
            GameTimer.TimerRemainResetToCool(skill_2);

            anim.SetInteger("skill", 2);
            Debug.Log("Skill 2 Used");
        }
    }
}
