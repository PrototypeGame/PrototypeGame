using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView3DAngle : MonoBehaviour {

    [SerializeField] private float viewAngle; // 시야각 (120도);
    [SerializeField] private float viewDistance; // 시야거리 (10미터);
    [SerializeField] private LayerMask targetMask; // 타겟 마스크 (플레이어)

    public void View()
    {
        Vector3 _leftBoundary = AngleToDirectionVector(-viewAngle * 0.5f, transform.eulerAngles.y);
        Vector3 _rightBoundary = AngleToDirectionVector(viewAngle * 0.5f, transform.eulerAngles.y);

        Debug.DrawRay(transform.position + transform.up, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.red);

        // 범위 내의 Enemy들 Detect
        Collider[] targets = DetectObjectWithPhysicsSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < targets.Length; i++)
        {
            Transform targetTransf = targets[i].transform;
            
            if(/* TODO: Tag 비교 기능 삽입 */)
            {
                float targetAngle = DirectionVectorToAngle(transform.position, targetTransf, transform.forward);
                // TODO: HERE
                if(targetAngle < viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewDistance))
                    {
                        if(_hit.transform.name == "Player")
                        {
                            Debug.Log("플레이어가 돼지 시야 내에 있습니다");
                            Debug.DrawRay(transform.position + transform.up, _direction, Color.blue);
                            thePig.Run(_hit.transform.position);
                        }
                    }
                }

            }
        }
    }

    private static Vector3 AngleToDirectionVector(float angle, float currentEulerAngleY)
    {
        angle += currentEulerAngleY;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    private static float DirectionVectorToAngle(Vector3 startVec, Vector3 destVec, Vector3 forwardVec)
    {
        return Vector3.Angle((destVec - startVec).normalized, forwardVec);
    }
}
