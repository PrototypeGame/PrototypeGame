//작성자 : 임자현  작성 시작일 2019-03-29   마지막 수정일 : 2019-03-30
using System.Collections;
using UnityEngine;

namespace Core
{
    public class DynamicCamera : MonoBehaviour
    {
        public Transform traceTarg;

        public float xMargin = 1;       // 캐릭터 이동시 카메라상의 좌표 여유
        public float yMargin = 1;
        public float xSmooth = 5.0f;    // 카메라 부드러운 이동
        public float ySmooth = 5.0f;

        public Vector2 maxAnd;          // 카메라 이동 최대 좌표
        public Vector2 minAnd;          // 카메라 이동 최소 좌표

        [Range(0, 2)]
        public float ShakeRadius = 0.3f;
        public float ShakeTime = 0.5f;

        private Transform camTr;
        private Camera cam;

        private float targetX;
        private float targetY;

        private Vector2 shakel;

        private float time;

        private void Awake()
        {
            camTr = GetComponent<Transform>();
            cam = GetComponent<Camera>();

            time = 1;
            cam.orthographicSize = 6.5f;
        }

        private void Update()
        {
            if (CheckXMargin())
                targetX = Mathf.Lerp(camTr.position.x, traceTarg.position.x, xSmooth * Time.deltaTime);
            if (CheckYMargin())
                targetY = Mathf.Lerp(camTr.position.y, traceTarg.position.y, ySmooth * Time.deltaTime);

            targetX = Mathf.Clamp(targetX, minAnd.x, maxAnd.x);
            targetY = Mathf.Clamp(targetY, minAnd.y, maxAnd.y);

            camTr.position = new Vector3(targetX + shakel.x, targetY + shakel.y, camTr.position.z);
        }

        private void TraceTarget()
        {

        }

        private bool CheckXMargin()         // 지정된 여유보다 크다면 Ture반환
        {
            return Mathf.Abs(camTr.position.x - traceTarg.position.x) > xMargin;
        }

        private bool CheckYMargin()         // 지정된 여유보다 크다면 Ture반환
        {
            return Mathf.Abs(camTr.position.y - traceTarg.position.y) > yMargin;
        }

        public void shake()         //카메라 흔들기
        {
            StartCoroutine(ShakeCamera());
        }

        private IEnumerator ShakeCamera()
        {
            Color temp = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            time = 0;
            while (time <= ShakeTime)
            {
                if (temp.a > 0.4f)
                {
                    temp.a = 0.4f;
                }
                else
                    temp.a += 0.05f;

                Vector2 insideUnitCircle = Random.insideUnitCircle;
                shakel = new Vector2(insideUnitCircle.x * ShakeRadius, insideUnitCircle.y * ShakeRadius);

                time += Time.deltaTime;
                yield return null;
            }

            temp.a = 0.0f;
            shakel.x = 0;
            shakel.y = 0;
        }
    }
}
