using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    /// <summary> 걷는 속도와 달리는 속도를 구분하는 기준속도 변수 </summary>
    private float speedSection;

    public static bool isMovable;
    private Vector3 clickPoint;

    private PlayerManager manager;

    private EnemyDetector detector;

    private Rigidbody rigid;
    private Animator anim;

    public GameTimer dashDelay;

    private LayerMask floorLayer;
    private LayerMask enemyLayer;
    private LayerMask itemLayer;

    private void Awake()
    {
        isMovable = true;

        manager = GetComponent<PlayerManager>();

        detector = GetComponentInChildren<EnemyDetector>();

        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        // 충돌 레이어 설정
        floorLayer = LayerMask.NameToLayer("Floor");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        itemLayer = LayerMask.NameToLayer("Item");
    }

    private void Update()
    {
        MovementInput();

        GameTimer.TimerOnGoing(dashDelay);
    }

    private void FixedUpdate()
    {
        MoveToPoint();
    }

    private RaycastHit hit;

    private void MovementInput()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonDown(0))
        {
            // 자동 공격 이동
            Debug.Log("[DEBUG] 클릭한 지점까지 움직이면서 범위안에 들어온 보스를 자동 공격합니다.");
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonDown(1))
        {
            // 보스 무시 이동
            Debug.Log("[DEBUG] 범위안에 들어온 보스를 무시하면서 클릭한 지점까지 움직입니다.");
        }
        // Layer에 따라 움직임
        if (Input.GetMouseButton(1))
        {
            if (RaycastUtil.FireRay(ref hit, floorLayer))
            {
                if (manager.moveSpeed <= speedSection)
                {
                    anim.SetFloat("speed", 0.5f);
                }
                else
                {
                    anim.SetFloat("speed", 1.0f);
                }

                clickPoint = hit.point;
                isMovable = true;

                Debug.Log("[DEBUG] 클릭하는 지점으로 이동합니다.");
            }
            else if (RaycastUtil.FireRay(ref hit, enemyLayer))
            {
                Vector3 temp = hit.transform.position - transform.position;
                temp.y = 0.0f;

                if (temp.sqrMagnitude <= Mathf.Pow(2, manager.attackRange))
                {
                    Debug.Log("[DEBUG] 클릭한 보스를 공격합니다.");
                }
                else
                {
                    Debug.Log("[DEBUG] 클릭한 보스를 공격하기 위해서는 좀더 다가가야 합니다.");
                }
            }
            else if (RaycastUtil.FireRay(ref hit, itemLayer))
            {
                Vector3 temp = hit.transform.position - transform.position;
                temp.y = 0.0f;

                if (temp.sqrMagnitude <= Mathf.Pow(2, manager.itemGetRange))
                {
                    Debug.Log("[DEBUG] 클릭한 아이템을 획득합니다.");
                }
                else
                {
                    Debug.Log("[DEBUG] 클릭한 아이템을 획득하기 위해서는 좀더 다각야 합니다.");
                }
            }
            else
            {
                isMovable = false;
            }
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashDelay.notInCool)
            {
                GameTimer.TimerRemainResetToCool(dashDelay);
                isMovable = false;
                anim.SetFloat("speed", 0.0f);

                MovementUtil.ForceDashMove(rigid, transform, ArrowControl.arrowDest, manager.dashPower, ForceMode.Impulse);
            }
        }

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

    private void MoveToPoint()
    {
        if (isMovable)
        {
            MovementUtil.Move(rigid, transform.position, clickPoint, manager.moveSpeed * Time.deltaTime);

            Vector3 dir = clickPoint - transform.position;
            dir.y = 0.0f;

            if (dir != Vector3.zero)
            {
                MovementUtil.Rotate(transform, dir, manager.rotateSpeed * Time.deltaTime * 100);
            }

            if (dir.sqrMagnitude < 0.1f * 0.1f)
            {
                anim.SetFloat("speed", 0.0f);
                isMovable = false;
            }
        }
    }
}
