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

        skillAutoOnGoing = false;
    }

    private void Update()
    {
        TimerUpdate();

        KeyInput();
    }

    public virtual void TimerUpdate()
    {
        foreach (var item in timer)
        {
            GameTimer.TimerOnGoing(item);
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
                    GameTimer.TimerRemainResetToCool(timer[1]);
                    Skill_Slot_1();
                }
            }
            // 키보드 컨트롤 2
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (timer[2].notInCool)
                {
                    GameTimer.TimerRemainResetToCool(timer[2]);
                    Skill_Slot_2();
                }
            }
            // 키보드 컨트롤 3
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (timer[3].notInCool)
                {
                    GameTimer.TimerRemainResetToCool(timer[3]);
                    Skill_Slot_3();
                }
            }
            // 키보드 컨트롤 4
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (timer[4].notInCool)
                {
                    GameTimer.TimerRemainResetToCool(timer[4]);
                    Skill_Ultimate();
                }
            }
        }
    }

    public virtual void Skill_Auto() { }
    public bool skillAutoOnGoing;
    public virtual void Skill_Slot_1() { }
    public bool skill1OnGoing;
    public virtual void Skill_Slot_2() { }
    public bool skill2OnGoing;
    public virtual void Skill_Slot_3() { }
    public bool skill3OnGoing;
    public virtual void Skill_Ultimate() { }
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
