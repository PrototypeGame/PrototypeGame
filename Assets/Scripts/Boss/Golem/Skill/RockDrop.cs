using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class RockDrop : AttackActionBase
    {
        public GameObject rockOb;
        public Transform target;
        public float fallSpeed = 50.0f; // 투사체 속도
        public float activeTime = 2;

        private RockDropping rock;  
        // Start is called before the first frame update
        void Awake()
        {
            rock = Instantiate(rockOb).GetComponent<RockDropping>();
            rock.hideFlags = HideFlags.HideInHierarchy;
            rock.gameObject.SetActive(false);
            rock.Initialize(attackRange, fallSpeed, activeTime, minDamage, maxDamage);
        }

        public override void ExcuteSkill()
        {
            Debug.Log("RockDrop");
            if (!rock.gameObject.activeSelf)
            {
                rock.transform.position = target.position;
                rock.gameObject.SetActive(true);
            }
        }
    }
}