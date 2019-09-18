using UnityEngine;
using System.Collections;

public class PlayerAnimStateSender : MonoBehaviour
{
    public bool tAttackAllow = false;

    public void AttackAllowSetFalse()
    {
        tAttackAllow = false;
    }

    public void AttackAllowSetTrue()
    {
        tAttackAllow = true;
    }
}
