using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    /// <summary> 걷는 속도와 달리는 속도를 구분하는 기준속도 변수 </summary>
    private float speedSection;

    public Vector3 destPoint;

    public float moveSpeed;
    public float rotateSpeed;
    public float dashPower;

    public bool isMovable;

    private PlayerManager manager;
    private PlayerAttack attack;

    public PointerManager pointer;

    private Rigidbody rigid;

    public TimerUtil dashDelay;

    private RaycastHit hit;

    private void Awake()
    {
        isMovable = false;

        manager = GetComponent<PlayerManager>();
        attack = GetComponent<PlayerAttack>();

        pointer = FindObjectOfType<PointerManager>();

        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        TimerUtil.TimerOnGoing(dashDelay);

        if (Input.anyKey)
        {
            // 혼합 컨트롤 1
            // 이동 + 자동공격
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (RayUtil.FireRay(ref hit, manager.floorLayer))
                    {
                        Debug.Log("A + Mouse 0");

                        destPoint = hit.point;

                        manager.isTargeted = false;
                        manager.isDetectable = true;

                        manager.anim.SetInteger("speed", 1);
                        isMovable = true;

                        pointer.SetState(PointState.DETECT_ENEMY);
                        pointer.SetPosition(hit.point, false);
                    }
                }
            }
            // 혼합 컨트롤 2
            // 이동 + Enemy Engore
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (RayUtil.FireRay(ref hit))
                    {
                        Debug.Log("Alt + Mouse 1");

                        destPoint = hit.point;

                        manager.isTargeted = false;
                        manager.isDetectable = false;

<<<<<<< HEAD
                        manager.anim.SetInteger("speed", 1);
                        isMovable = true;
=======
                    // 타겟 지정을 해제
                    manager.isTargeted = false;
                    // Enemy 탐지 비활성화
                    manager.isDetectable = false;
                    // 움직임 활성화
                    isMovable = true;
>>>>>>> 88d16e6c98473a1dcf25ca88e92c056f19a0edc4

                        pointer.SetState(PointState.IGNORE_ENEMY);
                        pointer.SetPosition(hit.point, false);
                    }
                }
            }
            // 마우스 입력
<<<<<<< HEAD
            else
=======
            if (!Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
>>>>>>> 88d16e6c98473a1dcf25ca88e92c056f19a0edc4
            {
                if (Input.GetMouseButtonDown(1))
                {
<<<<<<< HEAD
                    if (RayUtil.FireRay(ref hit))
=======
                    manager.anim.SetInteger("speed", 1);

                    if (hit.transform.root.gameObject.layer == manager.floorLayer)
>>>>>>> 88d16e6c98473a1dcf25ca88e92c056f19a0edc4
                    {
                        Debug.Log("Mouse 1");

<<<<<<< HEAD
                        int hitLayer = 1 << hit.transform.root.gameObject.layer;

                        if (manager.floorLayer == hitLayer)
                        {
                            destPoint = hit.point;

                            manager.isTargeted = false;
                            manager.isDetectable = false;

                            manager.anim.SetInteger("speed", 1);
                            isMovable = true;

                            pointer.SetState(PointState.MOVE);
                        }
                        else if (manager.enemyLayer == hitLayer)
                        {
                            // Target Set
                            manager.targetEnemy = hit.transform.root.GetComponent<EnemyManager>();

                            manager.isTargeted = true;
                            manager.isDetectable = false;
                            manager.isDetected = true;

                            manager.anim.SetInteger("speed", 1);
                            isMovable = true;

                            pointer.SetState(PointState.ENEMY);
                        }
                        else
                        {
                            // 아이템 처리
                            if (hit.transform.root.CompareTag("Item"))
                            {
                                destPoint = hit.point;

                                manager.isTargeted = false;
                                manager.isDetectable = false;

                                manager.anim.SetInteger("speed", 1);
                                isMovable = true;

                                pointer.SetState(PointState.DEFAULT);
                            }
                            else
                            {
                                isMovable = false;

                                pointer.SetState(PointState.DISABLE);
                            }
                        }

                        pointer.SetPosition(hit.point, false);
=======
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
>>>>>>> 88d16e6c98473a1dcf25ca88e92c056f19a0edc4
                    }

                    manager.isDetectable = false;

                    isMovable = true;
                }
            }
            // Dash
            // 키보드 컨트롤 5
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashDelay.notInCool)
                {
                    if (RayUtil.FireRay(ref hit, manager.floorLayer))
                    {
                        TimerUtil.TimerRemainResetToCool(dashDelay);
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

    private void FixedUpdate()
    {
        MoveToPoint();
        DetectRotate();
    }

    private void MoveToPoint()
    {
        if (isMovable)
        {
<<<<<<< HEAD
            MovementUtil.Move(rigid, transform.position, destPoint, moveSpeed * Time.deltaTime);
=======
            MovementUtil.Move(rigid, transform.position, clickPoint, manager.data.moveSpeed * Time.deltaTime);
>>>>>>> 88d16e6c98473a1dcf25ca88e92c056f19a0edc4

            Vector3 dir = destPoint - transform.position;
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
