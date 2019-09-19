using UnityEngine;
using System.Collections;

public class DetectUtil
{
    public static bool FireRay(ref RaycastHit hit, LayerMask hitLayer)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out hit, 1000.0f, 1 << hitLayer);
    }

    public static bool FireRay(ref RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out hit, 1000.0f);
    }

    public static void SetAttackSight(Camera sight, float distance, float length, float aspect)
    {
        sight.farClipPlane = distance;
        sight.fieldOfView = length * length;
        sight.aspect = aspect;
    }

    public static bool Detect(Camera sight, Collider col)
    {
        if (col == null)
            return false;

        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        Bounds bounds = col.bounds;

        return GeometryUtility.TestPlanesAABB(ps, bounds);
    }

    public static bool Detect(Camera sight, float distance, float length, float aspect, Collider col)
    {
        if (col == null)
            return false;

        // Detect 범위 수정
        SetAttackSight(sight, distance, length, aspect);

        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        Bounds bounds = col.bounds;

        return GeometryUtility.TestPlanesAABB(ps, bounds);
    }
}
