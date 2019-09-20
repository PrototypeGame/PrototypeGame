using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    /// <summary> 걷는 속도와 달리는 속도를 구분하는 기준속도 변수 </summary>
    private float speedSection;

    public Vector3 clickPoint;

    public float moveSpeed;
    public float rotateSpeed;
    public float dashPower;

    public bool isMovable;

    private PlayerManager manager;
    private PlayerAttack attack;

    private Rigidbody rigid;

    public GameTimer dashDelay;

    private void Awake()
    {
        isMovable = false;

        manager = GetComponent<PlayerManager>();
        attack = GetComponent<PlayerAttack>();

        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovementInput();
        TargetLock();

        GameTimer.TimerOnGoing(dashDelay);
    }

    private void FixedUpdate()
    {
        MoveToPoint();
        DetectRotate();
    }

    private RaycastHit hit;

    /// <summary> 키보드, 마우스 입력 </summary>
    private void MovementInput()
    {
        if (Input.anyKey)
        {
            // 혼합 컨트롤 1
            if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonDown(0))
            {
                if (DetectUtil.FireRay(ref hit))
                {
                    // 자동 공격 이동
                    Debug.Log("[DEBUG] 클릭한 지점까지 움직이면서 범위안에 들어온 보스를 자동 공격합니다.");

                    // 타겟 지정을 해제
                    manager.isTargeted = false;
                    // Enemy 탐지 활성화
                    manager.isDetectable = true;
                    // 움직임 활성화
                    isMovable = true;

                    clickPoint = hit.point;
                }
            }
            // 혼합 컨트롤 2
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonDown(1))
            {
                if (DetectUtil.FireRay(ref hit))
                {
                    // 보스 무시 이동
                    Debug.Log("[DEBUG] 범위안에 들어온 보스를 무시하면서 클릭한 지점까지 움직입니다.");

                    // 타겟 지정을 해제
                    manager.isTargeted = false;
                    // Enemy 탐지 비활성화
                    manager.isDetectable = false;
                    // 움직임 활성화
                    isMovable = true;

                    clickPoint = hit.point;
                }
            }
            // 마우스 입력
            if (!Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
            {
                manager.isTargeted = false;

                if (DetectUtil.FireRay(ref hit))
                {
                    manager.anim.SetInteger("speed", 1);

                    if (hit.transform.root.gameObject.layer == manager.floorLayer)
                    {
                        Debug.Log("바닥");
                        clickPoint = hit.point;

                        manager.isTargeted = false;
                    }
                    else if (hit.transform.root.gameObject.layer == manager.enemyLayer)
                    {
                        Debug.Log("적");
                        manager.isTargeted = true;
                        manager.isDetected = true;

                        manager.targetEnemy = hit.transform.root.GetComponent<EnemyManager>();
                    }
                    else
                    {
                        Debug.Log("그 이외");
                        clickPoint = hit.point;

                        manager.isTargeted = false;
                    }

                    manager.isDetectable = false;

                    isMovable = true;
                }
                else
                {
                    isMovable = false;
                }
            }

            // Dash
            // 키보드 컨트롤 5
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashDelay.notInCool)
                {
                    if (DetectUtil.FireRay(ref hit, manager.floorLayer))
                    {
                        GameTimer.TimerRemainResetToCool(dashDelay);
                        isMovable = false;

                        MovementUtil.ForceDashMove(rigid, transform, hit.point - transform.position, dashPower, ForceMode.Impulse);
                    }
                }
            }

            // 키보드 컨트롤 13
            // Stop Movement
            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move Cancel
                isMovable = false;

                // Force Cancel
                rigid.isKinematic = true;
                rigid.isKinematic = false;
            }
        }
    }

    private void TargetLock()
    {
        if (manager.isTargeted)
        {
            clickPoint = manager.targetEnemy.transform.position;
        }
        else
        {
            if (manager.targetEnemy != null)
            {
                manager.targetEnemy = null;
            }
        }
    }

    private void MoveToPoint()
    {
        if (isMovable)
        {
            MovementUtil.Move(rigid, transform.position, clickPoint, manager.data.moveSpeed * Time.deltaTime);

            Vector3 dir = clickPoint - transform.position;
            dir.y = 0.0f;

            if (dir != Vector3.zero)
            {
                MovementUtil.Rotate(transform, dir, rotateSpeed * Time.deltaTime * 100);
            }

            if (dir.sqrMagnitude < 0.1f * 0.1f)
            {
                manager.anim.SetInteger("speed", 0);
                isMovable = false;
            }
        }
    }

    private void DetectRotate()
    {
        // 탐지가 활성화 되어있으면 탐지가 되면 정지
        if (manager.DetectEnemy())
        {
            isMovable = false;

            Vector3 dir = manager.targetEnemy.transform.position - transform.position;
            dir.y = 0.0f;

            if (dir != Vector3.zero)
            {
                MovementUtil.Rotate(transform, dir, rotateSpeed * Time.deltaTime * 100);
            }
        }
    }
}
