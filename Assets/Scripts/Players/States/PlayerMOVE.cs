﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMOVE : PlayerFSMState
{
    private float horizMoveValue = 0.0f;
    private float vertMoveValue = 0.0f;

    private float rotateSpeed = 8.0f;

    private Vector3 moveDirection = Vector3.zero;

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
            InputValueUpdate();
            MoveRotate();
        }
        // Attack Check
        else if (PlayerInputController.CheckInputSignal(manager.attackKeys))
        {
            Debug.Log("[DEBUG] Attack Input detected");
            manager.SetPlayerState(PlayerableCharacterState.NORMALATTACK);
        }
        else
        {
            Debug.Log("[DEBUG] Move Input not detected");
            manager.SetPlayerState(PlayerableCharacterState.IDLE);
        }
    }

    public override void FSMFixedUpdate()
    {
        base.FSMFixedUpdate();


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

    public override void FSMAnimationPlay()
    {
        base.FSMAnimationPlay();
    }
}
