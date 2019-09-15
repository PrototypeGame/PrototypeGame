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

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        sight = GetComponentInChildren<Camera>();

        detectEnable = false;

        detectedEnemy = null;
        isDetected = false;
    }

    // 탐지된 Enemy 저장

    private void OnTriggerEnter(Collider other)
    {
        if (detectEnable)
        {
            // other가 Enemy이고 isDetected가 false이면..

            isDetected = true;
            detectedEnemy = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // other가 Enemy이고 isDetected가 true이면..

        isDetected = false;
        detectedEnemy = null;
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
