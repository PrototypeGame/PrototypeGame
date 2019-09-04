using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControl : MonoBehaviour
{
    private Camera mainCam;

    public Vector3 destLineVec;

    LayerMask rayHitLayer;

    public Transform LineTransform { get => transform; }

    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        rayHitLayer = 1 << LayerMask.NameToLayer("Floor");
    }

    private void Update()
    {
        RotateLine();
    }

    RaycastHit hit_line;

    private void RotateLine()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit_line, 1000, rayHitLayer))
        {
            destLineVec = hit_line.point - transform.position;
            destLineVec.y = 0.0f;
            destLineVec.Normalize();

            if (destLineVec != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(destLineVec);
        }
    }
}
