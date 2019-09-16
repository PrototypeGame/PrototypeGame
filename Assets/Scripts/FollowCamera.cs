using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTarget;

    /// <summary> 카메라가 따라갈 때의 높이 </summary>
    public float zFollowDist = 4.0f;
    /// <summary> 카메라가 따라갈 때의 높이 </summary>
    public float yFollowHeight = 3.5f;
    /// <summary> z축을 기준으로 도는 속도 </summary>
    public float yRotSpeed = 150.0f;
    /// <summary> Target을 따라가는 속도 </summary>
    public float followSpeed;

    private float followMarginRange = 0.05f;

    /// <summary> 카메라의 초기 위치값 </summary>
    public Vector3 initPos = new Vector3(0.0f, 0.0f, 0.0f);
    /// <summary> 카메라의 초기 회전값 </summary>
    public Vector3 initRot = new Vector3(0.0f, 0.0f, 0.0f);

    private void Start()
    {
        StartCoroutine(StartFollowTarget());
    }

    private void FixedUpdate()
    {
        Vector3 followPos = new Vector3(followTarget.position.x, yFollowHeight, followTarget.position.z - zFollowDist);
        Quaternion followLook = Quaternion.LookRotation(followPos);

        WaitForSeconds delay = new WaitForSeconds(0.002f);

        if (Vector3.Distance(transform.position, followPos) >= followMarginRange)
        {
            transform.position = Vector3.Lerp(transform.position, followPos, followSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, followLook, yRotSpeed * Time.deltaTime);
        }
    }

    private IEnumerator StartFollowTarget()
    {
        if (followTarget == null)
        {
            followTarget = GameObject.FindGameObjectWithTag("Player").transform.root;
        }

        Vector3 startPos = new Vector3(followTarget.position.x, yFollowHeight, followTarget.position.z - zFollowDist);
        Debug.Log("Start Moving");

        WaitForSeconds delay = new WaitForSeconds(0.002f);

        bool[] bCheck = new bool[2] { false, false };

        while (bCheck[0] && bCheck[1])
        {
            Vector3 dir = startPos - transform.position;

            if (dir.sqrMagnitude < followMarginRange * followMarginRange) {
                MovementUtil.Move(transform, transform.position, startPos, followSpeed * Time.deltaTime);
            }
            else {
                bCheck[0] = true;
            }

            if (dir != Vector3.zero) {
                MovementUtil.Rotate(transform, dir, yRotSpeed * Time.deltaTime);
            }
            else {
                bCheck[1] = true;
            }

            yield return delay;
        }
        Debug.Log("End Moving");

        yield break;
    }
}
