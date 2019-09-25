using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public Collider hitCol;
    public float hp;

    public Vector3[] points;
    public int pointNum;
    public int curPoint = 0;

    private void Awake()
    {
        pointNum = points.Length;
    }

    private void FixedUpdate()
    {
        MovementUtil.PointMove(transform, transform.position, points[curPoint], 2.0f * Time.deltaTime);

        Vector3 dir = points[curPoint] - transform.position;
        dir.y = 0.0f;

        if (dir.sqrMagnitude < 0.1f * 0.1f)
        {
            if (curPoint + 1 < pointNum)
            {
                curPoint++;
            }
            else
            {
                curPoint = 0;
            }
        }
    }
}
