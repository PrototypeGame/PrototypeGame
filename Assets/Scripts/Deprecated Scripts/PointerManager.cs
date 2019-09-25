using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PointState
{
    DEFAULT, MOVE, ENEMY, IGNORE_ENEMY, DETECT_ENEMY, DISABLE
}

public class PointerManager : MonoBehaviour
{
    public GameObject movePtr;
    public GameObject attackPtr;

    private Vector3 ptrPos;
    public PointState ptrState;

    private RaycastHit hit;

    private void Awake()
    {
        movePtr.SetActive(false);
        attackPtr.SetActive(false);
    }

    public void SetState(PointState stat)
    {
        switch (stat)
        {
            case PointState.DEFAULT:
                ptrState = stat;
                movePtr.SetActive(true);
                attackPtr.SetActive(false);
                break;
            case PointState.MOVE:
                ptrState = stat;
                movePtr.SetActive(true);
                attackPtr.SetActive(false);
                break;
            case PointState.ENEMY:
                ptrState = stat;
                movePtr.SetActive(false);
                attackPtr.SetActive(true);
                break;
            case PointState.IGNORE_ENEMY:
                ptrState = stat;
                movePtr.SetActive(true);
                attackPtr.SetActive(false);
                break;
            case PointState.DETECT_ENEMY:
                ptrState = stat;
                movePtr.SetActive(false);
                attackPtr.SetActive(true);
                break;
            case PointState.DISABLE:
                ptrState = stat;
                movePtr.SetActive(false);
                attackPtr.SetActive(false);
                break;
            default:
                ptrState = PointState.DEFAULT;
                movePtr.SetActive(false);
                attackPtr.SetActive(false);
                break;
        }
    }

    public void SetPosition(Vector3 pos, bool bCorrect)
    {
        if (bCorrect)
        {
            pos.y = 0.0f;
            ptrPos = pos;
            transform.position = pos;
        }
        else
        {
            ptrPos = pos;
            transform.position = pos;
        }
    }

    public void Disable()
    {
        movePtr.SetActive(false);
        attackPtr.SetActive(false);
    }
}
