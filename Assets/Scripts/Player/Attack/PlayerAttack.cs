using UnityEngine;
using System;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    protected PlayerManager manager;
    protected PlayerAnimStateSender animSender;
    
    [SerializeField]
    protected GameTimer[] timer = new GameTimer[5];

    protected virtual void Awake()
    {
        manager = GetComponent<PlayerManager>();
        animSender = GetComponentInChildren<PlayerAnimStateSender>();
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
                    StartCoroutine(Skill_Slot_1());
                }
            }
            // 키보드 컨트롤 2
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (timer[2].notInCool)
                {
                    GameTimer.TimerRemainResetToCool(timer[2]);
                    StartCoroutine(Skill_Slot_2());
                }
            }
            // 키보드 컨트롤 3
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (timer[3].notInCool)
                {
                    GameTimer.TimerRemainResetToCool(timer[3]);
                    StartCoroutine(Skill_Slot_3());
                }
            }
            // 키보드 컨트롤 4
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (timer[4].notInCool)
                {
                    GameTimer.TimerRemainResetToCool(timer[4]);
                    StartCoroutine(Skill_Ultimate());
                }
            }
        }
    }

    public virtual IEnumerator Skill_Auto() { yield return null; }
    public virtual IEnumerator Skill_Slot_1() { yield return null; }
    public virtual IEnumerator Skill_Slot_2() { yield return null; }
    public virtual IEnumerator Skill_Slot_3() { yield return null; }
    public virtual IEnumerator Skill_Ultimate() { yield return null; }
}
