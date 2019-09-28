using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    /// <summary> 걷는 속도와 달리는 속도를 구분하는 기준속도 변수 </summary>
    private float speedSection;

    public Vector3 destPoint;

    public float rotateSpeed;
    public float dashPower;

    public bool isMovable;

    private PlayerManager manager;
    private PlayerAttack attack;

    public PointerManager pointer;

    private Rigidbody rigid;

    public GameTimer dashDelay;

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
        TimerUtil.TimerCyclePlay(dashDelay);

        KeyInputControl();
        TargetFollowLock();
    }

    private void FixedUpdate()
    {
        MoveToPoint();
    }

    private void KeyInputControl()
    {
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
                    if (RayUtil.FireRay(ref hit, manager.floorLayer))
                    {
                        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
                        Debug.Log("Alt + Mouse 1");

                        destPoint = hit.point;

                        manager.isTargeted = false;
                        manager.isDetectable = false;

                        manager.anim.SetInteger("speed", 1);
                        isMovable = true;

                        pointer.SetState(PointState.IGNORE_ENEMY);
                        pointer.SetPosition(hit.point, false);
                    }
                }
            }
            // 마우스 입력
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (RayUtil.FireRay(ref hit))
                    {
                        Debug.Log("Mouse 1");

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
                    }
                }
            }
            // Dash
            // 키보드 컨트롤 5
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!TimerUtil.IsOnCoolTime(dashDelay))
                {
                    if (RayUtil.FireRay(ref hit, manager.floorLayer))
                    {
                        TimerUtil.TimerReset(dashDelay);
                        isMovable = false;

                        Vector3 dashPos = hit.point - transform.position;
                        dashPos.y = 0.0f;

                        MovementUtil.ForceDashMove(rigid, transform, dashPos, dashPower, ForceMode.Impulse);
                    }
                }
            }
            // 키보드 컨트롤 13
            // Stop Movement
            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move Cancel
                pointer.SetState(PointState.DISABLE);
                manager.anim.SetInteger("speed", 0);
                isMovable = false;

                // Force Cancel
                rigid.isKinematic = true;
                rigid.isKinematic = false;
            }
        }
    }

    private void MoveToPoint()
    {
        if (isMovable)
        {
            //MovementUtil.Move(rigid, transform.position, destPoint, manager.data.MoveSpeed * Time.deltaTime);

            Vector3 dir = destPoint - transform.position;
            dir.y = 0.0f;

            if (dir != Vector3.zero)
            {
                MovementUtil.Rotate(transform, dir, rotateSpeed * Time.deltaTime * 100);
            }

            if (dir.sqrMagnitude < 0.1f * 0.1f)
            {
                pointer.SetState(PointState.DISABLE);
                manager.anim.SetInteger("speed", 0);
                isMovable = false;
            }
        }
    }

    public void MoveStop()
    {
        manager.anim.SetInteger("speed", 0);
        isMovable = false;
    }

    public void MoveRestart()
    {
        manager.anim.SetInteger("speed", 1);
        isMovable = true;
    }

    private void TargetFollowLock()
    {
        if (manager.isTargeted)
        {
            // Target Enemy의 위치를 destPoint로 지정
            destPoint = manager.targetEnemy.transform.position;
            pointer.SetPosition(manager.targetEnemy.transform.position, true);
        }
        else
        {
            // Target이 해제되면 TargetEnemy를 null
            if (manager.targetEnemy != null)
                manager.targetEnemy = null;
        }
    }
}
