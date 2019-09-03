using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoClass;

public class PlayerAttack : MonoBehaviour
{
    public GameTimer normalAttack;
    public GameTimer skill_1;
    public GameTimer skill_2;

    public GameObject ball;

    private bool usingSkill;

    private void Awake()
    {
        usingSkill = false;
    }

    void Update()
    {
        KeyInput();
        MouseInput();

        GameTimer.TimerOnGoing(normalAttack);
        GameTimer.TimerOnGoing(skill_1);
        GameTimer.TimerOnGoing(skill_2);
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (normalAttack.notInCool == true)
            {
                NormalAttack();
            }
        }
    }

    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (skill_1.notInCool == true)
            {
                Skill_1();
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (skill_2.notInCool == true)
            {
                Skill_2();
            }
        }
    }

    public void NormalAttack()
    {
        GameTimer.TimerRemainResetToCool(normalAttack);

        Instantiate(ball, transform.position + Vector3.up, transform.rotation);
    }

    public void Skill_1()
    {
        GameTimer.TimerRemainResetToCool(skill_1);
        Debug.Log("Skill 1 Used");
    }

    public void Skill_2()
    {
        GameTimer.TimerRemainResetToCool(skill_2);
        Debug.Log("Skill 2 Used");
    }
}
