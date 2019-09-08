using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    // Alt 클릭에서는 비활성화
    public bool detectEnable;

    public Transform detectedEnemy;
    public float detectRange;
    public bool isDetected;

    private void Awake()
    {
        //detectEnable = false;

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
}
