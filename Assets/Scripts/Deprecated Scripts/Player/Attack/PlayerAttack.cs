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
    public GameTimer[] timer;

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
            TimerUtil.TimerCyclePlay(item);
        }
    }

    private void KeyInput()
    {
        if (Input.anyKeyDown)
        {
            // 키보드 컨트롤 1
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!TimerUtil.IsOnCoolTime(timer[1]))
                {
                    TimerUtil.TimerReset(timer[1]);
                    Skill_1_Anim();
                }
            }
            // 키보드 컨트롤 2
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (!TimerUtil.IsOnCoolTime(timer[2]))
                {
                    TimerUtil.TimerReset(timer[2]);
                    Skill_1_Anim();
                }
            }
            // 키보드 컨트롤 3
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (!TimerUtil.IsOnCoolTime(timer[3]))
                {
                    TimerUtil.TimerReset(timer[3]);
                    Skill_1_Anim();
                }
            }
            // 키보드 컨트롤 4
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (!TimerUtil.IsOnCoolTime(timer[4]))
                {
                    TimerUtil.TimerReset(timer[4]);
                    Skill_1_Anim();
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
