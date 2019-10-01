using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMOVE : PlayerFSMState
{
    private float horizMoveValue = 0.0f;
    private float vertMoveValue = 0.0f;

    private float rotateSpeed = 25.0f;

    private Vector3 moveDirection = Vector3.zero;

    private void OnEnable()
    {

    }

    public override void FSMStart()
    {
        base.FSMStart();


    }

    public override void FSMUpdate()
    {
        base.FSMUpdate();

        // Move Check
        if (InputControlUtil.CheckInputSignal(manager.inputManager.moveKeys))
        {
            if (GameKey.GetKeyDown(GameKeyPreset.Dash))
            {
                if (!TimerUtil.IsOnCoolTime(manager.timeManager.dashTimer))
                {
                    //Debug.Log("[DEBUG] Dash Input detected");
                    manager.SetPlayerState(PlayableCharacterState.DASH);
                }
            }

            //Debug.Log("[DEBUG] Move Input detected");
            InputValueUpdate();
        }
        // Attack Check
        else if (InputControlUtil.CheckInputSignal(manager.inputManager.attackKeys))
        {
           // Debug.Log("[DEBUG] Attack Input detected");
            manager.SetPlayerState(PlayableCharacterState.NORMALATTACK);
        }
        else if (InputControlUtil.CheckInputSignal(manager.inputManager.skillKeys))
        {
           // Debug.Log("[DEBUG] Skill Input detected");
            manager.SetPlayerState(PlayableCharacterState.SKILLATTACK);
            ((manager.currentFSMAction) as PlayerSKILLATTACK).SkillSelectRun(InputControlUtil.ReturnInputKey(manager.inputManager.skillKeys));
        }
        else
        {
            //Debug.Log("[DEBUG] Move Input not detected");
            manager.SetPlayerState(PlayableCharacterState.IDLE);
        }
    }

    public override void FSMFixedUpdate()
    {
        base.FSMFixedUpdate();

        MoveRotate();
    }

    private void InputValueUpdate()
    {
        horizMoveValue = InputControlUtil.HorizontalInputValue();
        vertMoveValue = InputControlUtil.VerticalInputValue();

        moveDirection.x = horizMoveValue;
        moveDirection.z = vertMoveValue;
        moveDirection = moveDirection.normalized;
    }

    private void MoveRotate()
    {
        MovementUtil.VectorMove(manager.rigid, moveDirection, manager.statusManager.MoveSpeed);
        MovementUtil.Rotate(manager.transf, moveDirection, rotateSpeed);
    }
}
