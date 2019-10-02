using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView3DAngle : MonoBehaviour
{
    [SerializeField] private float viewAngle; // 시야각 (120도);
    [SerializeField] private float viewDistance; // 시야거리 (10미터);

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private string enemyTag;

    private void Update()
    {
        View();
    }

    public void View()
    {
        Vector3 _leftBoundary = AngleToDirectionVector(-viewAngle * 0.5f, transform.eulerAngles.y);
        Vector3 _rightBoundary = AngleToDirectionVector(viewAngle * 0.5f, transform.eulerAngles.y);

        Debug.DrawRay(transform.position + transform.up, _leftBoundary.normalized * viewDistance, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary.normalized * viewDistance, Color.red);

        // 범위 내의 Enemy들 Detect
        Collider[] targets = DetectUtil.DetectObjectWithPhysicsSphere(transform.position, viewDistance, enemyLayer);

        for (int i = 0; i < targets.Length; i++)
        {
            if(targets[i].transform.CompareTag(enemyTag))
            {
                float enemyAngle = DirectionVectorToAngle(transform.position, targets[i].transform.position, transform.forward);
                // TODO: HERE
                if(enemyAngle < viewAngle * 0.5f)
                {
                    Debug.DrawRay(transform.position + transform.up, targets[i].transform.position - transform.position + transform.up, Color.blue);
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
