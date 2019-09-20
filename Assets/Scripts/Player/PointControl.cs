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

    public bool bFollowAllow = false;
    public Transform followObj;
        
    public PointState ptState;
    public GameObject[] Points;

    private Dictionary<PointState, GameObject> ptSet = new Dictionary<PointState, GameObject>();

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

        ClickPointStat();
    }

    private RaycastHit hit;
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
                }
                else if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonUp(0))
                {
                    SetPoint(PointState.ENEMY);
                }

                // 혼합 컨트롤 2
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonDown(1))
                {
                    SetPoint(PointState.MOVE);
                }
                else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonUp(1))
                {
                    SetPoint(PointState.MOVE);
                }

                // 마우스 입력
                if (Input.GetMouseButtonDown(1))
                {
                    if (hit.transform.CompareTag("Floor"))
                        SetPoint(PointState.MOVE);
                    else if (hit.transform.CompareTag("Enemy"))
                        SetPoint(PointState.ENEMY);
                    else if (hit.transform.CompareTag("Item"))
                        SetPoint(PointState.ITEM);
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    if (hit.transform.CompareTag("Floor"))
                        SetPoint(PointState.MOVE);
                    else if (hit.transform.CompareTag("Enemy"))
                        SetPoint(PointState.ENEMY);
                    else if (hit.transform.CompareTag("Item"))
                        SetPoint(PointState.ITEM);
                }
            }
            else
            {
                DisablePoint();
            }
        }
    }

    private void ClickPointStat()
    {
        if (bFollowAllow)
        {
            transform.position = followObj.position;
        }
        else
        {
            Vector3 hitPos = hit.point;
            hitPos.y = 0.0f;

            transform.position = hitPos;
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
