using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class RockfallRain : AttackActionBase
    {
        public GameObject rockOb;
        public int DropCount = 12;
        public float fallInterval = 0.05f;
        public float fallSpeed = 50.0f; // 투사체 속도
        public float activeTime = 2;

        private WaitForSeconds wait;
        private List<RockDropping> rockPool; 

        // Start is called before the first frame update
        void Awake()
        {
            rockPool = new List<RockDropping>();
            wait = new WaitForSeconds(fallInterval);

            for (int i = 0; i < DropCount; i++)
            {
                var rock = Instantiate(rockOb).GetComponent<RockDropping>();
                rock.hideFlags = HideFlags.HideInHierarchy;
                rock.gameObject.SetActive(false);
                rock.Initialize(attackRange, fallSpeed, activeTime, minDamage, maxDamage);
                rockPool.Add(rock);
            }
        }

        public override void ExcuteSkill()
        {
            Debug.Log("RockFallRain");
            StartCoroutine(Skill());
        }

        IEnumerator Skill()
        {
            for (int i = 0; i < DropCount; i++)
            {
                rockPool[i].gameObject.SetActive(true);
                rockPool[i].transform.position = RendomPoint(i);
                yield return wait;
            }
        }

        public Vector3 RendomPoint(int i)
        {
            float x = 0;
            float z = 0;
            if (i < DropCount * 0.25f)
            {
                x = Random.Range(-9.0f, -2.0f);
                z = Random.Range(2.0f, 9.0f);
            }

            else if (i < DropCount * 0.5f)
            {
                x = Random.Range(2.0f, 9.0f);
                z = Random.Range(2.0f, 9.0f);
            }

            else if (i < DropCount * 0.75f)
            {
                x = Random.Range(2.0f, 9.0f);
                z = Random.Range(-9.0f, -2.0f);
            }

            else
            {
                x = Random.Range(-9.0f, -2.0f);
                z = Random.Range(-9.0f, -2.0f);
            }

            Vector3 retVal;
            retVal.x = x;
            retVal.y = 1;
            retVal.z = z;
            return retVal;
        }
    }
}