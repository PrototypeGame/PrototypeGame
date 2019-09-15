using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PointState
{
    MOVE, ENEMY, ITEM
}

public class PointControl : MonoBehaviour
{
    public GameObject[] Points;

    private Vector3 pointPos;
    public PointState ptState;

    private Dictionary<PointState, GameObject> ptSet = new Dictionary<PointState, GameObject>();

    private RaycastHit hit;

    private LayerMask floorLayer;
    private LayerMask enemyLayer;
    private LayerMask itemLayer;

    private void Awake()
    {
        ptState = PointState.MOVE;
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
    }

    private void Update()
    {
        PointManage();
    }

    private void PointManage()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (DetectUtil.FireRay(ref hit))
            {
                Debug.Log("Down");
                if (hit.transform.CompareTag("Floor"))
                {
                    Debug.Log("Floor");
                    SetPoint(PointState.MOVE);
                }
                else if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy");
                    SetPoint(PointState.ENEMY);
                }
                else if (hit.transform.CompareTag("Item"))
                {
                    Debug.Log("Item");
                    SetPoint(PointState.ITEM);
                }

                Vector3 hitPos = hit.point;
                hitPos.y = 0.0f;

                transform.position = hitPos;
            }
            else
            {
                DisablePoint();
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (DetectUtil.FireRay(ref hit))
            {
                Debug.Log("Up");
                if (hit.transform.CompareTag("Floor"))
                {
                    Debug.Log("Floor");
                    SetPoint(PointState.MOVE);
                }
                else if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy");
                    SetPoint(PointState.ENEMY);
                }
                else if (hit.transform.CompareTag("Item"))
                {
                    Debug.Log("Item");
                    SetPoint(PointState.ITEM);
                }

                Vector3 hitPos = hit.point;
                hitPos.y = 0.0f;

                transform.position = hitPos;
            }
            else
            {
                DisablePoint();
            }
        }
    }

    public void SetPoint(PointState stat)
    {
        ptState = stat;

        foreach (var item in ptSet)
        {
            item.Value.SetActive(false);
        }

        ptSet[stat].SetActive(true);
    }

    public void DisablePoint()
    {
        foreach (var item in ptSet)
        {
            item.Value.SetActive(false);
        }
    }
}
