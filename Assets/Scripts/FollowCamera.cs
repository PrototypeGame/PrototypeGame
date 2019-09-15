using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTarget;

    /// <summary> 카메라와 타겟의 거리 </summary>
    public float dist = 4.0f;

    /// <summary> z축을 기준으로 도는 속도 </summary>
    public float zRotSpeed = 150.0f;

    /// <summary> 카메라의 초기위치 </summary>
    public Vector3 initPos = new Vector3(0.0f, 0.0f, 0.0f);

    /// <summary>
    /// Angle의 최소, 최대 제한
    /// </summary>
    /// <param name="angle">각도</param>
    /// <param name="min">최소 각도</param>
    /// <param name="max">최대 각도</param>
    /// <returns>최소와 최대 사이의 각도로 조절되어 리턴된다</returns>
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
