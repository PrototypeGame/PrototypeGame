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
            RendomPoint(DropCount);
            for (int i = 0; i < DropCount; i++)
            {
                rockPool[i].gameObject.SetActive(true);
                rockPool[i].transform.position = tempV[i];
                yield return wait;
            }
            tempV.Clear();
        }

        private List<Vector3> tempV = new List<Vector3>();
        public void RendomPoint(int DropCount)
        {
            Vector3 retVal;
            bool cheak;
            for (int i = 0; i < DropCount; i++)
            {
                do
                {
                    cheak = false;
                    Vector2 ranV = Random.insideUnitCircle * 12.3f;

                    retVal.x = ranV.x;
                    retVal.y = 1;
                    retVal.z = ranV.y;
                    for (int c = 0; c < tempV.Count; c++)
                    {
                        if ((tempV[c] - retVal).sqrMagnitude <= 3.0f * 3.0f)
                        {
                            cheak |= true;
                        }
                    }

                } while (cheak);

                tempV.Add(retVal);
            }
        }
    }
}