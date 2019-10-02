// 작성자: 임자현
// 작성일: 2019-09-06
// 설명: 보스 몬스터들의 베이스

using UnityEngine;

// 동기화 정보
// 보스의 좌표
// 사용 스킬
// 범위 공격 범위 동기화
// 발사체 동기화

namespace Boss
{
    //public enum BossState
    //{
    //    Idle,
    //    Tracing,
    //    NomalAttack,
    //    ExcuteSkill,
    //    Grogi,
    //    Dead
    //};

    public class BossMonsterBase : MonoBehaviour
    {
        public int curHP;
        public int maxHP;
        public float moveSpeed;

        private bool isDead = false;

        public void OnDamage(int damage)
        {
            curHP -= damage;
        }

        public bool OnDead()
        {
            if (curHP <= 0)
                return true;

            return false;
        }
    }
}
