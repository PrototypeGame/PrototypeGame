using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static bool isMovable;
    private Vector3 clickPoint;

    private PlayerData data;
    private Attack attack;

    private EnemyDetector detector;

    private Rigidbody rigid;
    private Animator anim;

    private LayerMask floorLayer;
    private LayerMask enemyLayer;

    public GameTimer dashDelay;

    public GameObject pointer;

    private void Awake()
    {
        isMovable = true;

        data = GetComponent<PlayerData>();
        attack = GetComponent<Attack>();

        detector = GetComponentInChildren<EnemyDetector>();

        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        floorLayer = 1 << LayerMask.NameToLayer("Floor");
    }

    private void Update()
    {
        ControlInput();
        EnemyTrace();

        GameTimer.TimerOnGoing(dashDelay);
    }

    private void FixedUpdate()
    {
        MoveToPoint();
    }

    private void ControlInput()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, floorLayer))
            {
                if (data.moveSpeed <= 4)
                {
                    anim.SetFloat("speed", 0.5f);
                }
                else
                {
                    anim.SetFloat("speed", 1.0f);
                }
                clickPoint = hit.point;
                isMovable = true;
            }
            else
                isMovable = false;

            //if (!Input.GetKey(KeyCode.LeftAlt))
            //{
            //    if (Physics.Raycast(ray, out hit, 1000, enemyLayer))
            //    {
            //        // 공격대상으로 지정
            //        detector.isDetected = true;
            //        detector.detectedEnemy = hit.transform.root;
            //    }
            //}
        }

        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, floorLayer))
            {
                pointer.transform.position = hit.point;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashDelay.notInCool)
            {
                GameTimer.TimerRemainResetToCool(dashDelay);
                isMovable = false;
                anim.SetFloat("speed", 0.0f);

                MovementUtil.ForceDashMove(rigid, transform, ArrowControl.arrowDest, data.dashPower, ForceMode.Impulse);
                //MovementUtil.TeleportMove(transform, ArrowControl.arrowDest, data.dashPower);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // Move Cancel
            isMovable = false;

            // Force Cancel
            rigid.isKinematic = true;
            rigid.isKinematic = false;
        }
    }

    private void MoveToPoint()
    {
        if (isMovable)
        {
            MovementUtil.Move(rigid, transform.position, clickPoint, data.moveSpeed * Time.deltaTime);

            Vector3 dir = clickPoint - transform.position;
            dir.y = 0.0f;

            if (dir != Vector3.zero)
            {
                MovementUtil.Rotate(transform, dir, data.rotateSpeed * Time.deltaTime * 100);
            }

            if (dir.sqrMagnitude < 0.1f * 0.1f)
            {
                anim.SetFloat("speed", 0.0f);
                isMovable = false;
            }
        }
    }

    private void EnemyTrace()
    {
        if (detector.isDetected)
        {
            // Enemy Trace

            Vector3 playerPos = transform.position;
            playerPos.y = 0.0f;
            Vector3 enemyPos = detector.detectedEnemy.position;
            enemyPos.y = 0.0f;

            if ((playerPos - enemyPos).sqrMagnitude <= attack.attackRange)
            {
                // Enemy Attack
                // Attack에서 공격 대상으로 지정
            }
            else
            {
                // 공격대상에서 해제
            }
        }
    }
}
