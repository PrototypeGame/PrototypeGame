using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    protected PlayerManager manager;

    [SerializeField]
    protected GameTimer[] timer;

    // 공격 대상
    public EnemyManager target;

    private int enemyNum = 0;
    protected EnemyManager[] enemys;

    protected virtual void Awake()
    {
        manager = GetComponent<PlayerManager>();

        // 몬스터 등록
        foreach (var item in FindObjectsOfType<EnemyManager>())
        {
            enemys[enemyNum++] = item;
        }
    }

    private void Update()
    {
        TimerUpdate();

        KeyInput();

        AttackTarget();
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
            if (Input.GetKeyDown(KeyCode.Q))
                Skill_Slot_1();
            else if (Input.GetKeyDown(KeyCode.W))
                Skill_Slot_2();
            else if (Input.GetKeyDown(KeyCode.E))
                Skill_Slot_3();
            else if (Input.GetKeyDown(KeyCode.R))
                Skill_Ultimate();
        }
    }

    private void AttackTarget()
    {
        if (target != null)
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
