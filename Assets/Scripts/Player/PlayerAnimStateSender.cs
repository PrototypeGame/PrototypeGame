using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerAnimStateSender : MonoBehaviour
{
    public UnityEvent skillAutoEvent;

    public void LinkAnim_Skill_Auto()
    {
        skillAutoEvent.Invoke();
    }
}
