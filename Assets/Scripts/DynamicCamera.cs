//작성자 : 임자현  작성 시작일 2019-03-29   마지막 수정일 : 2019-03-30
using System.Collections;
using UnityEngine;

namespace Core
{
    public class DynamicCamera : MonoBehaviour
    {
        public Transform cameraTarget;
        public Transform midleBackGround;
        public Transform longBackGround;

        //public UnityEngine.UI.Image EditorBlackScreen;

        public float xMargin = 1; // 캐릭터 이동시 카메라상의 좌표 여유
        public float yMargin = 1;
        public float xSmooth = 5.0f; // 카메라 부드러운 이동                                    
        public float ySmooth = 5.0f;
        public Vector2 maxAnd;// 카메라 이동 최대 좌표
        public Vector2 minAnd; // 카메라 이동 최소 좌표

        [Range(0, 2)]
        public float ShakeRadius = 0.3f;
        public float ShakeTime = 0.5f;

        private Transform camtr;
        private Camera cam;
        private float targetX;
        private float targetY;
        private Vector2 shakel;
        private float time;

        // Start is called before the first frame update
        void Awake()
        {
            camtr = GetComponent<Transform>();
            cam = GetComponent<Camera>();
            time = 1;
            cam.orthographicSize = 6.5f;
        }

        // Update is called once per frame
        void Update()
        {

            if (CheckXMargin())
                targetX = Mathf.Lerp(camtr.position.x, cameraTarget.position.x, xSmooth * Time.deltaTime);
            if (CheckYMargin())
                targetY = Mathf.Lerp(camtr.position.y, cameraTarget.position.y, ySmooth * Time.deltaTime);

            targetX = Mathf.Clamp(targetX, minAnd.x, maxAnd.x);
            targetY = Mathf.Clamp(targetY, minAnd.y, maxAnd.y);

            camtr.position = new Vector3(targetX + shakel.x, targetY + shakel.y, camtr.position.z);
            
            // 2D Cam
            //midleBackGround.position = new Vector3((targetX - 20) * 0.7f, targetY * 0.6f, 0.0f);
            //longBackGround.position = new Vector3((targetX - 10) * 0.9f, targetY * 0.8f, 0.0f);
        }

        bool CheckXMargin()// 지정된 여유보다 크다면 Ture반환
        {
            return Mathf.Abs(camtr.position.x - cameraTarget.position.x) > xMargin;
        }

        bool CheckYMargin()// 지정된 여유보다 크다면 Ture반환
        {
            return Mathf.Abs(camtr.position.y - cameraTarget.position.y) > yMargin;
        }

        public void shake() //카메라 흔들기
        {
            //EditorBlackScreen.gameObject.SetActive(true);
            StartCoroutine(ShakeCamera());
        }

        IEnumerator ShakeCamera()
        {
            Color temp = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            //EditorBlackScreen.color = temp;
            time = 0;
            while (time <= ShakeTime)
            {
                if (temp.a > 0.4f)
                {
                    temp.a = 0.4f;
                }
                else
                    temp.a += 0.05f;
                //EditorBlackScreen.color = temp;
                shakel = (Vector2)UnityEngine.Random.insideUnitCircle * ShakeRadius;
                time += Time.deltaTime;
                yield return null;
            }
            temp.a = 0.0f;
            //EditorBlackScreen.gameObject.SetActive(false);
            //EditorBlackScreen.color = temp;
            shakel.x = 0;
            shakel.y = 0;
        }
    }
}
