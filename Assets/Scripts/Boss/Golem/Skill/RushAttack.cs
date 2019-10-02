using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAttack : AttackActionBase
{
    public Projector projection;
    public Transform Target;
    public Transform mTran;

    public float activeTime = 2;
    public float RushSpeed = 10.0f;

    private float ratioPerSec = 0.0f;
    private float rockStartY = 0.0f;

    Vector3 temp;

    void Awake()
    {
        mTran = transform.root;

        projection.gameObject.SetActive(false);

        projection.aspectRatio = 0.01f;
        projection.orthographicSize = attackRange;
        ratioPerSec = (1.0f / activeTime);
    }

    void Update()
    {
        transform.position = mTran.position;
        if (projection.gameObject.activeSelf)
        {
            projection.aspectRatio += ratioPerSec * Time.deltaTime;

            if (projection.aspectRatio >= 0.75f)
            {
                projection.aspectRatio = 0.1f;
                projection.gameObject.SetActive(false);
                StartCoroutine(Rush());
            }
        }
    }
     
    public override void ExcuteSkill()
    {
        Debug.Log("RushAttack");
        temp = Target.position;
        Vector3 dir = Target.position - mTran.position;
        dir.y = 0.0f;

        mTran.rotation = Quaternion.LookRotation(dir);
        projection.gameObject.SetActive(true);
    }

    IEnumerator Rush()
    {
        Vector3 dir = mTran.position - temp;
        while (dir.sqrMagnitude > 0.2f * 0.2f)
        {
            mTran.position = Vector3.MoveTowards(mTran.position, temp, RushSpeed * Time.deltaTime);
            dir = mTran.position - temp;
            yield return null;
        }
        yield return null;
    }
}
