using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Camera mainCam;

    private Rigidbody rigid;
    private Animator anim;

    private PlayerAttack attack;
    private LineControl arrowLine;

    private bool moveAllow;

    public float moveSpeed;
    public float rotateSpeed;

    LayerMask floorLayer;

    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        attack = GetComponent<PlayerAttack>();
        arrowLine = GetComponentInChildren<LineControl>();

        moveAllow = false;

        floorLayer = 1 << LayerMask.NameToLayer("Floor");
    }

    private void Update()
    {
        ClickControll();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    RaycastHit hit_move;

    private void ClickControll()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit_move, 1000, floorLayer))
            {
                if (moveSpeed <= 4)
                {
                    anim.SetFloat("speed", 0.5f);
                }
                else
                {
                    anim.SetFloat("speed", 1.0f);
                }
                moveAllow = true;
            }
            else
                moveAllow = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // 공격 성공시 움직임을 멈춤
            if (attack.NormalAttack(arrowLine.destLineVec))
            {
                transform.rotation = arrowLine.LineTransform.rotation;
                anim.SetFloat("speed", 0.0f);
                moveAllow = false;
            }
        }
    }

    private void Move()
    {
        if (moveAllow)
        {
            Vector3 deltaMove = Vector3.MoveTowards(transform.position, hit_move.point, moveSpeed * Time.deltaTime);
            rigid.MovePosition(deltaMove);

            Vector3 dir = hit_move.point - transform.position;
            dir.y = 0;

            if (dir != Vector3.zero)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime * 100);

            if (dir.sqrMagnitude < 0.1f * 0.1f)
            {
                anim.SetFloat("speed", 0.0f);
                moveAllow = false;
            }
        }
    }

    private void Rotate()
    {
        //if (!moveAllow)
        //{
        //    transform.rotation = arrowLine.LineTransform.rotation;
        //}
    }
}
