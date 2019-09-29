using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAttack : AttackActionBase
{
    public Transform Target;
    public Transform mTran;

    public float activeTime = 2;
    public float RushSpeed = 10.0f;

    private float ratioPerSec = 0.0f;
    private float rockStartY = 0.0f;

    Vector3 temp;
    // Start is called before the first frame update
    void Awake()
    {
       // mTran = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       // transform.position = mTran.position;
    }

    public override void ExcuteSkill()
    {
        Debug.Log("RushAttack");
        temp = Target.position;
        Vector3 dir = Target.position - mTran.position;
        dir.y = 0.0f;

        mTran.rotation = Quaternion.LookRotation(dir);
        StartCoroutine(Rush());
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
        Debug.Log("And");
    }
}
