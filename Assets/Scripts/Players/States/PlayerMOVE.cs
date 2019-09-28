using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMOVE : PlayerFSMState
{
    private float horizMoveValue = 0.0f;
    private float vertMoveValue = 0.0f;

    private float rotateSpeed = 15.0f;

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
        if (PlayerInputController.CheckInputSignal(manager.moveKeys))
        {
            Debug.Log("[DEBUG] Move Input detected");
            InputValueUpdate();
        }
        else if (GameKey.GetKeyDown(GameKeyPreset.Dash))
        {
            Debug.Log("[DEBUG] Dash Input detected");
            manager.SetPlayerState(PlayableCharacterState.DASH);
        }
        // Attack Check
        else if (PlayerInputController.CheckInputSignal(manager.attackKeys))
        {
            Debug.Log("[DEBUG] Attack Input detected");
            manager.SetPlayerState(PlayableCharacterState.NORMALATTACK);
        }
        else if (PlayerInputController.CheckInputSignal(manager.skillKeys))
        {
            Debug.Log("[DEBUG] Skill Input detected");
            manager.SetPlayerState(PlayableCharacterState.SKILLATTACK);
            ((manager.currentFSMAction) as PlayerSKILLATTACK).SkillSelectRun(PlayerInputController.ReturnInputKey(manager.skillKeys));
        }
        else
        {
            Debug.Log("[DEBUG] Move Input not detected");
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
        horizMoveValue = PlayerInputController.HorizontalInputValue();
        vertMoveValue = PlayerInputController.VerticalInputValue();

        moveDirection.x = horizMoveValue;
        moveDirection.z = vertMoveValue;
        moveDirection = moveDirection.normalized;
    }

    private void MoveRotate()
    {
        MovementUtil.VectorMove(manager.rigid, moveDirection, manager.status.MoveSpeed);
        MovementUtil.Rotate(manager.transf, moveDirection, rotateSpeed);
    }
}
