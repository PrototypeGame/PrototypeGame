using UnityEngine;
using System;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    protected PlayerManager manager;
    protected PlayerControl control;
    protected PlayerAnimStateSender animSender;

    public Camera sight;

    // 거리
    public float attackDistance;
    // 범위 (가로, 세로길이)
    public float attackLength;

    [SerializeField]
    public TimerUtil[] timer;

    protected virtual void Awake()
    {
        manager = GetComponent<PlayerManager>();
        control = GetComponent<PlayerControl>();
        animSender = GetComponentInChildren<PlayerAnimStateSender>();

        sight = GetComponentInChildren<Camera>();

        DetectUtil.SetAttackSight(sight, attackDistance, attackLength, 1.0f);
    }

    public virtual void Update()
    {
        TimerUpdate();

        KeyInput();

        Skill_Auto_Anim();
    }

    public virtual void TimerUpdate()
    {
        foreach (var item in timer)
        {
            TimerUtil.TimerOnGoing(item);
        }
    }

    private void KeyInput()
    {
        if (Input.anyKeyDown)
        {
            // 키보드 컨트롤 1
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (timer[1].notInCool)
                {
                    TimerUtil.TimerRemainResetToCool(timer[1]);
                    Skill_1_Anim();
                }
            }
            // 키보드 컨트롤 2
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (timer[2].notInCool)
                {
                    TimerUtil.TimerRemainResetToCool(timer[2]);
                    Skill_2_Anim();
                }
            }
            // 키보드 컨트롤 3
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (timer[3].notInCool)
                {
                    TimerUtil.TimerRemainResetToCool(timer[3]);
                    Skill_3_Anim();
                }
            }
            // 키보드 컨트롤 4
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (timer[4].notInCool)
                {
                    TimerUtil.TimerRemainResetToCool(timer[4]);
                    Skill_Ultimate_Anim();
                }
            }
        }
    }

    public virtual void Skill_Auto_Anim() { }
    public virtual void Skill_Auto_Main() { }
    public virtual void Skill_1_Anim() { }
    public virtual void Skill_1_Main() { }
    public bool skill1OnGoing;
    public virtual void Skill_2_Anim() { }
    public virtual void Skill_2_Main() { }
    public bool skill2OnGoing;
    public virtual void Skill_3_Anim() { }
    public virtual void Skill_3_Main() { }
    public bool skill3OnGoing;
    public virtual void Skill_Ultimate_Anim() { }
    public virtual void Skill_Ultimate_Main() { }
    public bool skillUltimateOnGoing;

    private void OnDrawGizmos()
    {
        if (sight != null)
        {
            Gizmos.color = Color.red;
            Matrix4x4 temp = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(
                sight.transform.position,
                sight.transform.rotation,
                Vector3.one
                );
            Gizmos.DrawFrustum(Vector3.zero, sight.fieldOfView, sight.farClipPlane, sight.nearClipPlane, 1);
            Gizmos.matrix = temp;
        }
    }
}
