//작성자 : 임자현  작성 시작일 2019-03-29   마지막 수정일 : 2019-03-30
using System.Collections;
using UnityEngine;

namespace Core
{
    public class DynamicCamera : MonoBehaviour
    {
        public Transform cameraTarget;

        public float xMargin = 1; // 캐릭터 이동시 카메라상의 좌표 여유
        public float zMargin = 1;
        public float xSmooth = 5.0f; // 카메라 부드러운 이동                                    
        public float zSmooth = 5.0f;
        public Vector3 maxAnd;// 카메라 이동 최대 좌표
        public Vector3 minAnd; // 카메라 이동 최소 좌표

        [Range(0, 2)]
        public float ShakeRadius = 0.3f;
        public float ShakeTime = 0.5f;

        private Transform camtr;
        private Camera cam;
        private float targetX;
        private float targetZ;
        private Vector2 shakel;
        private float time;

        private Vector3 originPos;
        // Start is called before the first frame update
        void Awake()
        {
            camtr = GetComponent<Transform>();
            cam = GetComponent<Camera>();
            time = 1;
            originPos = camtr.position;
            //cam.orthographicSize = 6.5f;
        }

        // Update is called once per frame
        void Update()
        {

            if (CheckXMargin())
                targetX = Mathf.Lerp(camtr.position.x, originPos.x, xSmooth * Time.deltaTime);
            if (CheckYMargin())
                targetZ = Mathf.Lerp(camtr.position.z, originPos.z, zSmooth * Time.deltaTime);

            targetX = Mathf.Clamp(targetX, minAnd.x, maxAnd.x);
            targetZ = Mathf.Clamp(targetZ, minAnd.z, maxAnd.z);

            camtr.position = new Vector3(originPos.x + targetX + shakel.x, originPos.y, originPos.z + targetZ + shakel.y);

           // midleBackGround.position = new Vector3((targetX - 20) * 0.7f, targetY * 0.6f, 0.0f);
           // longBackGround.position = new Vector3((targetX - 10) * 0.9f, targetY * 0.8f, 0.0f);
        }

        bool CheckXMargin()// 지정된 여유보다 크다면 Ture반환
        {
            return Mathf.Abs(originPos.x - cameraTarget.position.x) > xMargin;
        }

        bool CheckYMargin()// 지정된 여유보다 크다면 Ture반환
        {
            return Mathf.Abs(originPos.z - cameraTarget.position.z) > zMargin;
        }

        public void shake() //카메라 흔들기
        {
           // EditorBlackScreen.gameObject.SetActive(true);
            StartCoroutine(ShakeCamera());
        }

        IEnumerator ShakeCamera()
        {
            //EditorBlackScreen.color = temp;
            time = 0;
            while (time <= ShakeTime)
            {
                shakel = (Vector2)UnityEngine.Random.insideUnitCircle * ShakeRadius;
                time += Time.deltaTime;
                yield return null;
            }
            shakel.x = 0;
            shakel.y = 0;
        }
    }
}
