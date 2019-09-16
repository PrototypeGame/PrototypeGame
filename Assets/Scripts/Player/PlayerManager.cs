using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerData data;

    public Animator anim;

    public Camera sight;

    public float itemGetRange;

    // Alt 클릭에서는 비활성화
    public bool detectEnable;

    public EnemyManager detectedEnemy;
    public float detectRange;
    public bool isDetected;

    public LayerMask floorLayer;
    public LayerMask enemyLayer;
    public LayerMask itemLayer;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        sight = GetComponentInChildren<Camera>();

        detectEnable = false;

        detectedEnemy = null;
        isDetected = false;

        // 충돌 레이어 설정
        floorLayer = LayerMask.NameToLayer("Floor");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        itemLayer = LayerMask.NameToLayer("Item");
    }

    // 탐지된 Enemy 저장

    private void OnTriggerEnter(Collider other)
    {
        if (detectEnable)
        {
            if (other.gameObject.layer == (1 << enemyLayer))
            {
                isDetected = true;
                detectedEnemy = other.transform.root.GetComponent<EnemyManager>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == (1 << enemyLayer))
        {
            isDetected = false;
            detectedEnemy = null;
        }
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
