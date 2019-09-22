using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
    public int detectedEnemyNum = 0;
    // 탐지 된 Enemy들
    public List<EnemyManager> detectedEnemys = new List<EnemyManager>();

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
        TargetLock();
    }

    private RaycastHit hit;
    private void ArrowControl()
    {
        if (RayUtil.FireRay(ref hit))
            arrow.rotation = Quaternion.LookRotation((hit.point - arrow.position).normalized);
    }

    public bool DetectEnemy()
    {
        // TODO: Enemy를 Detect 했을때 처리 작업할 것
        if (isDetectable)
        {
            Debug.Log(detectedEnemys);

            Vector3 curPlayerPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
            Vector3 tempEnemyPos;

            EnemyManager tempTarget;
            // 첫번째 거리의 비교 몬스터는 리스트 중 첫번째 노드에 있는것을 사용
            float minBetweenDist = Vector3.Distance(curPlayerPos, enemys[0].transform.position);

            foreach (var item in enemys)
            {
                // Enemy의 몬스터 위치
                tempEnemyPos = new Vector3(item.transform.root.position.x, 0.0f, item.transform.root.position.z);

                // Enemy가 탐지 거리 안에 있는 경우
                if (Vector3.Distance(curPlayerPos, tempEnemyPos) <= enemyDetectRange)
                {
                    // Detected List에 없다면 추가
                    if (!detectedEnemys.Contains(item))
                    {
                        detectedEnemys.Add(item);
                        detectedEnemyNum++;

                        Assert.IsTrue(detectedEnemyNum > enemyNum, "[ERROR] Detected Enemy's num is more then total enemy's num");
                    }
                }
                // Enemy가 탐지 거리를 초과해 밖에 있는 경우
                else
                {
                    // 이미 List에 있다면 제거
                    if (detectedEnemys.Contains(item))
                    {
                        detectedEnemys.Remove(item);
                        detectedEnemyNum--;

                        Assert.IsTrue(detectedEnemyNum < 0, "[ERROR] Detected Enemy's num is less then 0");
                    }
                }

                // Enemy 중 가장 가까운 몬스터를 타겟으로 둠
                if (Vector3.Distance(curPlayerPos, tempEnemyPos) < minBetweenDist)
                {
                    minBetweenDist = Vector3.Distance(curPlayerPos, tempEnemyPos);
                    tempTarget = item;
                }
            }

            // 저장된 Enemy의 수가 1 이상이면 탐지됨을 나타냄
            if (detectedEnemyNum > 0)
            {
                return isDetected = true;
            }
            else
                return isDetected = false;
        }
        // Enemy Detect가 활성화 되지 않아 감지된 Enemy를 false로 표시
        else
            return isDetected = false;
    }

    private void TargetLock()
    {
        if (isTargeted)
        {
            // Target Enemy의 위치를 destPoint로 지정
            control.destPoint = targetEnemy.transform.position;
            control.pointer.SetPosition(targetEnemy.transform.position, true);
        }
        else
        {
            // Target이 해제되면 TargetEnemy를 null
            if (targetEnemy != null)
                targetEnemy = null;
        }
    }
}
