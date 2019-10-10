using System.Collections;
using UnityEngine;

namespace Boss
{
    public class RockRockScript : InteractCollision3D
    {
        public ParticleSystem particle;
        public float range;

        private GolemBehavior golem;
        private int damage;
        bool damageFlag = true;

        public override void OnPCollision(GameObject go)
        {
            Vector3 temp = this.transform.position;
            temp.y = 0.1f;
            if ((temp - golem.closePlayerTrans.position).sqrMagnitude < range * range && damageFlag)
            {
                golem.camShake.Invoke();
                golem.playerOnDamage.Invoke(damage);
                damageFlag = false;
            }

            StartCoroutine(ActiveAuto());
        }

        public void SetRockInfo(GolemBehavior golem, int damage)
        {
            this.golem = golem;
            this.damage = damage;
        }

        IEnumerator ActiveAuto()
        {
            yield return new WaitForSeconds(0.73f);
            //while (particle.isPlaying)
            //{
            //    yield return null;
            //}
            this.transform.root.gameObject.SetActive(false);
            damageFlag = true;
        }
    }
}