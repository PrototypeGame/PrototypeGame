using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public static Vector3 arrowDest;
    public static bool isFollowEnemy;

    private LayerMask rayHitLayer;

    RaycastHit hit;

    private void Awake()
    {
        isFollowEnemy = false;

        rayHitLayer = LayerMask.NameToLayer("Floor");
    }

    private void Update()
    {
        if (!isFollowEnemy)
        {
            RotateLine();
        }
    }

    private void RotateLine()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, 1 << rayHitLayer))
        {
            arrowDest = hit.point - transform.position;
            arrowDest.y = 0.0f;
            arrowDest.Normalize();

            if (arrowDest != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(arrowDest);
        }
    }
}
