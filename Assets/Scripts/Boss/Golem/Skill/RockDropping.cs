using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Boss
{
    public class RockDropping : ProjectileObjectBase
    {
        public Projector attackChargingPro;
        public Projector attackRangePro;

        public Transform rock;

        private float orSizePerSec = 0.0f;
        private float rockStartY = 0.0f;
        // Start is called before the first frame update
        void OnEnable()
        {
            attackChargingPro.orthographicSize = 0.0f;
            attackRangePro.orthographicSize = attackRange;
            rock.GetComponent<SphereCollider>().radius = attackRange;
            orSizePerSec = (attackRange / activeTime);
            rockStartY = (fallSpeed * activeTime);
            rock.position = new Vector3(rock.position.x, rockStartY, rock.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            attackChargingPro.orthographicSize += orSizePerSec * Time.deltaTime;
           
            if (attackChargingPro.orthographicSize >= attackRange)
            {
                attackChargingPro.orthographicSize = attackRange;
            }
            rock.Translate(Vector3.down * fallSpeed * Time.deltaTime);

            if (rock.position.y <= 0.0f)
                this.gameObject.SetActive(false);
        }
    }
}