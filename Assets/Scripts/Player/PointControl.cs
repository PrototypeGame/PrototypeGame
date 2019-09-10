using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum PointState
{
    MOVE, ENEMY, ITEM
}

public class PointControl : MonoBehaviour
{
    public GameObject[] Points;
    private GameObject curPoint;

    private Vector3 pointPos;

    private PointState ptState;

    private Dictionary<PointState, GameObject> ptSet;

    private RaycastHit hit;

    private LayerMask floorLayer;
    private LayerMask enemyLayer;
    private LayerMask itemLayer;

    private void Awake()
    {
        ptSet = new Dictionary<PointState, GameObject>();
        InitPoints();

        // 충돌 레이어 설정
        floorLayer = LayerMask.NameToLayer("Floor");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        itemLayer = LayerMask.NameToLayer("Item");
    }

    private void InitPoints()
    {
        ptSet[PointState.MOVE] = Points[0];
        ptSet[PointState.ENEMY] = Points[1];
        ptSet[PointState.ITEM] = Points[2];

        foreach (var item in ptSet)
        {
            item.Value.SetActive(false);
        }

        curPoint = Points[0];
    }

    private void Update()
    {
        PointManage();
    }

    private void PointManage()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (RaycastUtil.FireRay(ref hit, floorLayer))
            {
                SetPoint(PointState.MOVE);
            }
            else if (RaycastUtil.FireRay(ref hit, enemyLayer))
            {
                SetPoint(PointState.ENEMY);
            }
            else if (RaycastUtil.FireRay(ref hit, itemLayer))
            {
                SetPoint(PointState.ITEM);
            }

            transform.position = hit.point;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            transform.position = hit.point;
        }
    }

    private void SetPoint(PointState stat)
    {
        foreach (var item in ptSet)
        {
            item.Value.SetActive(false);
        }

        curPoint = ptSet[stat];
        curPoint.SetActive(true);
    }
}
