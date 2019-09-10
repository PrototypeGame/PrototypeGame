using UnityEngine;
using System.Collections;

public class RaycastUtil
{
    RaycastUtil()
    {
        maxRayDistance = 1000.0f;
    }

    public static float maxRayDistance;

    public static bool FireRay(ref RaycastHit hit, LayerMask hitLayer)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out hit, maxRayDistance, 1 << hitLayer);
    }

    public static bool FireRay(ref RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out hit, maxRayDistance);
    }
}
