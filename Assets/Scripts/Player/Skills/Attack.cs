using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    protected Animator anim;
    [SerializeField]
    protected GameTimer[] Timer;

    // 공격 대상
    protected GameObject attackTarget;

    public float attackRange;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        TimerUpdate();
        KeyInput();
        AttackTarget();
    }

    public virtual void TimerUpdate()
    {
        foreach (var item in Timer)
        {
            GameTimer.TimerOnGoing(item);
        }
    }

    private void KeyInput()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButton(0))
            {
                Skill_Auto();

                //if (Input.GetKey(KeyCode.A))
                //{
                //
                //}
            }
                
            if (Input.GetKeyDown(KeyCode.Q))
                Skill_Slot_1();
            if (Input.GetKeyDown(KeyCode.W))
                Skill_Slot_2();
            if (Input.GetKeyDown(KeyCode.E))
                Skill_Slot_3();
            if (Input.GetKeyDown(KeyCode.R))
                Skill_Ultimate();
        }
    }

    private void AttackTarget()
    {
        if (attackTarget != null)
        {
            // Enemy 공격
        }
    }

    public virtual void Skill_Auto() { }
    public virtual void Skill_Slot_1() { }
    public virtual void Skill_Slot_2() { }
    public virtual void Skill_Slot_3() { }
    public virtual void Skill_Ultimate() { }
}
