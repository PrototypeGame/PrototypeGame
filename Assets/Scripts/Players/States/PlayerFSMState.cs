using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSMState : MonoBehaviour
{
    protected PlayerFSMManager manager;

    public string stateName;

    private void Awake()
    {
        manager = GetComponent<PlayerFSMManager>();
    }

    public virtual void FSMStart() { }
    public virtual void FSMUpdate() { }
    public virtual void FSMFixedUpdate() { }

    public virtual void FSMAnimationPlay()
    {
        // Play Animation
        manager.animCtrl.DefaultPlayOnStateChange(manager.CurrentState);
    }
}
