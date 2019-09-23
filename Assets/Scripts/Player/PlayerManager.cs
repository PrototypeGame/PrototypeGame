using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;
using Debug = UnityEngine.Debug;

public class PlayerManager : MonoBehaviour
{
    public PlayerData data;
    private PlayerControl control;

    public Animator anim;

    public Transform arrow;

    public float itemGetRange;

    // Enemy Detect 관련 변수
    public bool isDetectable = false;
    public bool isDetected = false;
    public float enemyDetectRange;

    // Enemy 저장 변수
    public int enemyNum = 0;
    // 보스의 매니저 클래스의 이름에 따라 EnemyManager 이름 변경
    public List<EnemyManager> enemys = new List<EnemyManager>();
    // 탐지 된 Enemy 숫자
    //public int detectedEnemyNum = 0;
    // 탐지 된 Enemy들
    //public List<EnemyManager> detectedEnemys = new List<EnemyManager>();

    // 몬스터가 사용자 지정된 경우
    public bool isTargeted = false;
    public EnemyManager targetEnemy;

    // 충돌 레이어 설정
    public LayerMask floorLayer;
    public LayerMask enemyLayer;

    private void Awake()
    {
        control = GetComponent<PlayerControl>();

        anim = GetComponentInChildren<Animator>();

        floorLayer = 1 << LayerMask.NameToLayer("Floor");
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");

        InitStatus();

        AllEnemyRegister();
    }

    private void AllEnemyRegister()
    {
        // 몬스터 등록
        foreach (var item in FindObjectsOfType<EnemyManager>())
        {
            enemys.Add(item);
            enemyNum++;
        }
    }

    private void Update()
    {
        DetectEnemy();

        ArrowControl();
    }

    private void InitStatus()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Debug.Log("Player Status Setting Now...");

        data.SetMoveSpeed();
        Debug.Log("MoveSpeed : " + data.MoveSpeed);

        data.SetHealth();
        Debug.Log("Health : " + data.Health);
        data.SetMagicPoint();
        Debug.Log("MagicPoint : " + data.MagicPoint);

        data.SetAttackMinPower();
        Debug.Log("AttackMinPower : " + data.AttackMinPower);
        data.SetAttackMaxPower();
        Debug.Log("AttackMaxPower : " + data.AttackMaxPower);

        data.SetAttackSpeed();
        Debug.Log("AttackSpeed : " + data.AttackSpeed);

        data.SetArmor();
        Debug.Log("Armor : " + data.Armor);
        data.SetAvoidRate();
        Debug.Log("AvoidRate : " + data.AvoidRate);

        sw.Stop();
        Debug.Log("Status Setting End - During Time : " + sw.ElapsedMilliseconds.ToString() + " ms");
    }

    private RaycastHit hit;
    private void ArrowControl()
    {
        if (RayUtil.FireRay(ref hit))
            arrow.rotation = Quaternion.LookRotation((hit.point - arrow.position).normalized);
    }

    private void DetectEnemy()
    {
        if (isDetectable)
        {
            if (FindClosestEnemy(transform.position, enemyDetectRange))
            {
                isTargeted = true;
                isDetectable = false;
            }
        }
    }

    private bool FindClosestEnemy(Vector3 findPivot, float range)
    {
        foreach (var item in enemys)
        {
            Vector3 enemyPos = item.transform.position;
            enemyPos.y = 0.0f;

            if (Vector3.Distance(findPivot, enemyPos) <= range)
            {
                targetEnemy = item;
            }
        }

        if (targetEnemy != null)
        {
            return isDetected = true;
        }
        else
        {
            return isDetected = false;
        }
    }

    public void TargetDestroy()
    {
        if (enemys.Remove(targetEnemy))
        {
            enemyNum--;

            EnemyManager temp = targetEnemy;

            targetEnemy = null;
            isTargeted = false;

            Destroy(temp);
        }
    }

    //private bool FindEnemyInRange(Vector3 findPivot, float range)
    //{
    //    detectedEnemys.Clear();
    //
    //    foreach (var item in enemys)
    //    {
    //        Vector3 enemyPos = item.transform.position;
    //        enemyPos.y = 0.0f;
    //
    //        if (Vector3.Distance(findPivot, enemyPos) <= range)
    //        {
    //            detectedEnemys.Add(item);
    //            detectedEnemyNum++;
    //        }
    //    }
    //
    //    if (detectedEnemyNum > 0)
    //    {
    //        targetEnemy = FindClosestEnemy(findPivot);
    //        return isDetected = true;
    //    }
    //    else
    //    {
    //        targetEnemy = null;
    //        return isDetected = false;
    //    }
    //}
    //
    //private EnemyManager FindClosestEnemy(Vector3 findPivot)
    //{
    //    EnemyManager closestEnemy = null;
    //    Vector3 closestEnemyPos = Vector3.zero;
    //    findPivot.y = 0.0f;
    //
    //    if (isDetected)
    //    {
    //        foreach (var item in detectedEnemys)
    //        {
    //            Vector3 enemyPos = item.transform.position;
    //            enemyPos.y = 0.0f;
    //
    //            if (closestEnemy == null)
    //            {
    //                closestEnemy = item;
    //                closestEnemyPos = item.transform.position;
    //                closestEnemyPos.y = 0.0f;
    //            }
    //            else
    //            {
    //                if (Vector3.Distance(findPivot, enemyPos) < Vector3.Distance(findPivot, closestEnemyPos))
    //                {
    //                    closestEnemy = item;
    //                    closestEnemyPos = item.transform.position;
    //                    closestEnemyPos.y = 0.0f;
    //                }
    //            }
    //        }
    //        return closestEnemy;
    //    }
    //    else
    //        return null;
    //}
}
