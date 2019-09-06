using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Camera mainCam;
    
    private Animator anim;

    private PlayerData data;
    private PlayerControl control;
    private PlayerAttack attack;
    private LineControl arrowLine;

    public LayerMask floorLayer;

    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        anim = GetComponentInChildren<Animator>();

        data = GetComponent<PlayerData>();
        control = GetComponent<PlayerControl>();
        attack = GetComponent<PlayerAttack>();
        arrowLine = GetComponentInChildren<LineControl>();

        floorLayer = 1 << LayerMask.NameToLayer("Floor");
    }

    private void Update()
    {
        ClickControll();
    }

    RaycastHit hit_move;

    private void ClickControll()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit_move, 1000, floorLayer))
            {
                if (data.moveSpeed <= 4)
                {
                    anim.SetFloat("speed", 0.5f);
                }
                else
                {
                    anim.SetFloat("speed", 1.0f);
                }
                control.clickPoint = hit_move.point;
                control.isMovable = true;
            }
            else
                control.isMovable = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // 공격 성공시 움직임을 멈춤
            if (attack.NormalAttack(arrowLine.destLineVec))
            {
                transform.rotation = arrowLine.LineTransform.rotation;
                anim.SetFloat("speed", 0.0f);
                control.isMovable = false;
            }
        }
    }
}
