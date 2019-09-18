using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerData data;
    private PlayerControl control;

    public Animator anim;

    public Transform arrow;
    public Camera sight;

    public float itemGetRange;

    // Enemy 관련 변수
    public bool isAttackable = false;

    public bool isDetectable = false;
    public bool isDetected = false;
    public float attackableEnemyRange;

    public bool isTargeted = false;
    public Transform targetedEnemy;

    // Enemy 저장 변수
    public int enemyNum = 0;
    // 보스의 매니저 클래스의 이름에 따라 EnemyManager 이름 변경
    public List<EnemyManager> enemys = new List<EnemyManager>();
    public int detectedEnemyNum = 0;
    public List<EnemyManager> detectedEnemys = new List<EnemyManager>();

    public LayerMask floorLayer;
    public LayerMask enemyLayer;

    private void Awake()
    {
        control = GetComponent<PlayerControl>();

        anim = GetComponentInChildren<Animator>();

        sight = GetComponentInChildren<Camera>();

        // 몬스터 등록
        foreach (var item in FindObjectsOfType<EnemyManager>())
        {
            enemys.Add(item);
            enemyNum++;
        }

        // 충돌 레이어 설정
        floorLayer = LayerMask.NameToLayer("Floor");
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void Update()
    {
        ArrowControl();
    }

    private RaycastHit hit;
    private void ArrowControl()
    {
        if (DetectUtil.FireRay(ref hit))
        {
            arrow.rotation = Quaternion.LookRotation((hit.point - arrow.position).normalized);
        }
    }

    public bool DetectEnemy()
    {
        // TODO: Enemy를 Detect 했을때 처리 작업할 것
        if (isDetectable)
        {
            if (!isDetected)
            {
                // 탐지한 Enemy의 숫자를 0으로 초기화
                detectedEnemyNum = 0;

                // 커다란 원의 내부에 Enemy가 있으면 Collider를 저장
                Collider[] enemyCols = Physics.OverlapSphere(transform.position, attackableEnemyRange, 1 << enemyLayer);

                // Enemy의 Collider에서 EnemyManager를 불러와 탐지된 Enemy들을 저장
                foreach (var item in enemyCols)
                {
                    detectedEnemys.Add(item.transform.root.GetComponent<EnemyManager>());
                    detectedEnemyNum++;
                }

                // 저장된 Enemy의 수가 1 이상이면 탐지됨을 나타냄
                if (detectedEnemyNum != 0)
                {
                    isDetectable = false;
                    return isDetected = true;
                }
                else
                {
                    return isDetected = false;
                }
            }
        }

        return isDetected = false;
    }

    private void OnDrawGizmos()
    {
        if (sight != null)
        {
            Gizmos.color = Color.red;
            Matrix4x4 temp = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(
                sight.transform.position,
                sight.transform.rotation,
                Vector3.one
                );
            Gizmos.DrawFrustum(Vector3.zero, sight.fieldOfView, sight.farClipPlane, sight.nearClipPlane, 1);
            Gizmos.matrix = temp;
        }
    }
}
