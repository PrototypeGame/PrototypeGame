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
}
