using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetectUtil
{
    public static Collider[] DetectObjectWithPhysicsSphere(Vector3 center, float radius, LayerMask mask)
    {
        return Physics.OverlapSphere(center, radius, mask);
    }
    public static Collider[] DetectObjectsWithPhysicsSphere(Vector3 center, float radius, LayerMask[] masks)
    {
        List<Collider> detectObjects = null;
        foreach (LayerMask mask in masks)
        {
            detectObjects.AddRange(Physics.OverlapSphere(center, radius, mask));
        }
        
        return detectObjects.ToArray();
    }

    public static List<Transform> DetectObjectsTransformWithAngle(List<Transform> transfList, Transform player, float detectAngle, float detectDistance)
    {
        Vector3 _leftBoundary = MathUtil.AngleToDirectionVector(-detectAngle * 0.5f, player.eulerAngles.y);
        Vector3 _rightBoundary = MathUtil.AngleToDirectionVector(detectAngle * 0.5f, player.eulerAngles.y);

        Debug.DrawRay(player.position + player.up, _leftBoundary.normalized * detectDistance, Color.red);
        Debug.DrawRay(player.position + player.up, _rightBoundary.normalized * detectDistance, Color.red);

        // 범위 내의 Enemy들 Detect
        List<Transform> targets = null;

        foreach (var item in transfList)
        {
            if (Vector3.Distance(player.position, item.position) <= detectDistance)
            {
                float enemyAngle = MathUtil.DirectionVectorToAngle(player.position, item.position, player.forward);
                // TODO: HERE
                if (enemyAngle < detectAngle * 0.5f)
                {
                    targets.Add(item);
                    Debug.DrawRay(player.position + player.up, item.position - player.position + player.up, Color.blue);
                }
            }
        }

        return targets;
    }
}
