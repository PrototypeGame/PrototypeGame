using UnityEngine;
using System.Collections;

public class RaycastUtil
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
}
