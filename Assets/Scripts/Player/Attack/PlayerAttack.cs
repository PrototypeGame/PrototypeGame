using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    protected PlayerManager manager;
    
    [SerializeField]
    protected GameTimer[] timer = new GameTimer[5];

    protected virtual void Awake()
    {
        manager = GetComponent<PlayerManager>();
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
                Skill_Slot_1();
            // 키보드 컨트롤 2
            else if (Input.GetKeyDown(KeyCode.W))
                Skill_Slot_2();
            // 키보드 컨트롤 3
            else if (Input.GetKeyDown(KeyCode.E))
                Skill_Slot_3();
            // 키보드 컨트롤 4
            else if (Input.GetKeyDown(KeyCode.R))
                Skill_Ultimate();
        }
    }

    public virtual void Skill_Auto() { }
    public virtual void Skill_Slot_1() { }
    public virtual void Skill_Slot_2() { }
    public virtual void Skill_Slot_3() { }
    public virtual void Skill_Ultimate() { }
}
