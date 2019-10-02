using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : AttackActionBase
{
    public Projector projection;
    public Transform Target;
    public Transform mTran;

    public float activeTime = 2;
    private float ratioPerSec = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        mTran = transform.root;

        projection.gameObject.SetActive(false);

        projection.aspectRatio = 0.01f;
        projection.orthographicSize = attackRange;
        ratioPerSec = (1.0f / activeTime);
    }

    // Update is called once per frame
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
            }
        }
    }

    public override void ExcuteSkill()
    {
        Debug.Log("EarthQuake");
        Vector3 dir = Target.position - mTran.position;
        dir.y = 0.0f;

        mTran.rotation = Quaternion.LookRotation(dir);
        projection.gameObject.SetActive(true);
    }
}
