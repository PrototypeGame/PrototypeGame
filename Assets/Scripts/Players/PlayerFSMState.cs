using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSMState : MonoBehaviour
{
    private PlayerFSMManager manager;

    public string stateName;

    protected virtual void Awake()
    {
        manager = GetComponent<PlayerFSMManager>();
    }

    public virtual void FSMStart()
    {
        // Play Animation
        manager.animCtrl.DefaultPlayOnStateChange(manager.CurrentState);
    }
    public virtual void FSMUpdate() { }
    public virtual void FSMFixedUpdate() { }
}
