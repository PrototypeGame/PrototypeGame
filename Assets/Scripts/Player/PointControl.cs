using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PointState
{
    MOVE, ENEMY, ITEM
}

public class PointControl : MonoBehaviour
{
    private PlayerManager manager;

    public GameObject[] Points;

    private Vector3 pointPos;
    public PointState ptState;

    private Dictionary<PointState, GameObject> ptSet = new Dictionary<PointState, GameObject>();

    private RaycastHit hit;

    private void Awake()
    {
        manager = FindObjectOfType<PlayerManager>();

        ptState = PointState.MOVE;
        InitPoints();
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
        if (Input.anyKey)
        {
            if (DetectUtil.FireRay(ref hit))
            {
                // 혼합 컨트롤 1
                if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonDown(0))
                {
                    SetPoint(PointState.ENEMY);

                    Vector3 hitPos = hit.point;
                    hitPos.y = 0.0f;

                    transform.position = hitPos;
                }
                else if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonUp(0))
                {
                    SetPoint(PointState.ENEMY);

                    Vector3 hitPos = hit.point;
                    hitPos.y = 0.0f;

                    transform.position = hitPos;
                }

                // 혼합 컨트롤 2
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonDown(1))
                {
                    SetPoint(PointState.MOVE);

                    Vector3 hitPos = hit.point;
                    hitPos.y = 0.0f;

                    transform.position = hitPos;
                }
                else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonUp(1))
                {
                    SetPoint(PointState.MOVE);

                    Vector3 hitPos = hit.point;
                    hitPos.y = 0.0f;

                    transform.position = hitPos;
                }

                // 마우스 입력
                if (Input.GetMouseButtonDown(1))
                {
                    if (hit.transform.CompareTag("Floor"))
                    {
                        SetPoint(PointState.MOVE);
                    }
                    else if (hit.transform.CompareTag("Enemy"))
                    {
                        SetPoint(PointState.ENEMY);
                    }
                    else if (hit.transform.CompareTag("Item"))
                    {
                        SetPoint(PointState.ITEM);
                    }

                    Vector3 hitPos = hit.point;
                    hitPos.y = 0.0f;

                    transform.position = hitPos;
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    if (hit.transform.CompareTag("Floor"))
                    {
                        SetPoint(PointState.MOVE);
                    }
                    else if (hit.transform.CompareTag("Enemy"))
                    {
                        SetPoint(PointState.ENEMY);
                    }
                    else if (hit.transform.CompareTag("Item"))
                    {
                        SetPoint(PointState.ITEM);
                    }

                    Vector3 hitPos = hit.point;
                    hitPos.y = 0.0f;

                    transform.position = hitPos;
                }
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
