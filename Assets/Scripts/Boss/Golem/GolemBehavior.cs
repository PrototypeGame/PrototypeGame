using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace Boss
{
    public class BossPattern
    {
        public int nextPatternHp;
        public int[] weight;
        public AttackActionBase[] skill;
    }

    public class GolemBehavior : BossMonsterBase
    {
        public enum GolemSkill
        {
            Earthquake,
            RushAttack,
            RockDrop,
            ShockWave,
            RockfallRain,
            RockThrow
        };

        [SerializeField]
        private int phase = 1;

        public float excuteSkillTiem = 1.5f;
        public float idleWaitTime = 1.5f;
        public float castingRisesTime = 3.0f;
        public float attackCastingRises = 0.1f;
        public float jointExceptDistance = 10.0f;
        public Transform[] playerTransforms; // 유저들의 위치
        public BossPattern[] patterns;
        public UnityEngine.Events.UnityEvent camShake;


        private float castingGauge = 0.0f;
        private Transform closePlayerTrans; // 가까운 유저의 위치
        private Transform bossTrans;
        private Animator anim;
        private Root ai;

        private void Awake()
        {
            bossTrans = GetComponent<Transform>();
            anim = GetComponent<Animator>();

            MovingObjectSMB<GolemBehavior>.Initialize(anim, this);

            #region Pattern
            patterns = new BossPattern[3];
            BossPattern phase = new BossPattern();

            phase.weight = new int[] { 3, 5, 8, 10 };
            phase.skill = new AttackActionBase[4];
            phase.nextPatternHp = maxHP/2;
            phase.skill[0] = (GetComponentInChildren<Earthquake>());
            phase.skill[1] = (GetComponentInChildren<RushAttack>());
            phase.skill[2] = (GetComponentInChildren<RockDrop>());
            phase.skill[3] = (GetComponentInChildren<ShockWave>());
            patterns[0] = phase;

            phase = new BossPattern();
            phase.weight = new int[] { 2, 4, 7, 10 };
            phase.nextPatternHp = maxHP/5;
            phase.skill = new AttackActionBase[4];
            phase.skill[0] = (GetComponentInChildren<RushAttack>());
            phase.skill[1] = (GetComponentInChildren<ShockWave>());
            phase.skill[2] = (GetComponentInChildren<RockfallRain>());
            phase.skill[3] = (GetComponentInChildren<RockThrow>());
            patterns[1] = phase;

            phase = new BossPattern();
            phase.weight = new int[] { 1, 2, 6, 10 };
            phase.skill = new AttackActionBase[4];
            phase.skill[0] = (GetComponentInChildren<RushAttack>());
            phase.skill[1] = (GetComponentInChildren<ShockWave>());
            phase.skill[2] = (GetComponentInChildren<RockfallRain>());
            phase.skill[3] = (GetComponentInChildren<RockThrow>());
            patterns[2] = phase;
            #endregion

            #region Behavior Tree
            ai = new Root();
            ai.AddBranch(
                new Action(ClosePlayerResearch),
                new Repeat(patterns.Length).AddBranch(

                    new While(ReplacePattern).AddBranch(
                        new ConditionalBranch(CastingSuccess).AddBranch(
                            new SetBool(anim, "Run", false),
                            new Wait(excuteSkillTiem),
                            new SetTrigger(anim, "Skill"),
                            new Wait(1.3f),
                            new Action(ExcuteSkill),
                            new WaitAnimationAnd(anim, "Idle"),
                            new Wait(idleWaitTime),
                            new Action(ClosePlayerResearch),
                            new Action(ResetCastingGage)
                            ),

                        new SetBool(anim, "Run"),

                        new ConditionalBranch(MeleeAttackCheak).AddBranch(
                            new SetBool(anim, "Run", false),
                            new SetTrigger(anim, "MeleeAttack"),
                            new WaitAnimationAnd(anim, "Idle"),
                            new Wait(idleWaitTime),
                            new Action(ClosePlayerResearch)
                        )
                    )
                ),
                new SetTrigger(anim, "Dead"),
                new Wait(4.0f),
                new Action(()=> { Destroy(this.gameObject); }),
                new Abort()
            );
            #endregion
        }

        private void Update()
        {
            ai.Tick();
            CastingSuccess();
        }

        public bool ReplacePattern()
        {
            if (OnDead())
                return false;

            if (curHP <= patterns[phase - 1].nextPatternHp)
            {
                phase++;
                PDebug.Log($"{phase} 페이즈 시작");
                return false;
            }

            return true;
        }

        public void ExcuteSkill()
        {
            RandomSkillSelect(patterns[phase - 1]);
            camShake.Invoke();
        }

        private int curPattern = 1;
        int select = 0;
        public void RandomSkillSelect(BossPattern pattern)
        {
            while (curPattern == select)
            {
                int randV = Random.Range(0, 100);

                if (pattern.weight[0] * 10 >= randV)
                    select = 0;

                else if (pattern.weight[1] * 10 >= randV)
                    select = 1;

                else if (pattern.weight[2] * 10 >= randV)
                    select = 2;

                else
                    select = 3;
            }

            curPattern = select;
            pattern.skill[curPattern].ExcuteSkill();
        }

        // 가장 가까운 플레이어 검색
        public void ClosePlayerResearch()
        {
            float distanceTemp = 1000.0f;
            for (int i = 0; i < playerTransforms.Length; i++)
            {
                float curDis = Vector3.SqrMagnitude(playerTransforms[i].position - bossTrans.position);

                if (curDis < distanceTemp)
                {
                    closePlayerTrans = playerTransforms[i];
                    distanceTemp = curDis;
                }
            }
        }

        // 플레이어와의 거리 계산
        public Vector3 DirectionFromPlayer()
        {
            Vector3 dir = closePlayerTrans.position - bossTrans.position;
            dir.y = 0;
            return dir;
        }

        // 플레이어 쫒기
        public void TracingClosePlayer()
        {
            Vector3 destination = closePlayerTrans.position;
            destination.y = bossTrans.position.y;

            Vector3 dir = DirectionFromPlayer();

            bossTrans.position = Vector3.MoveTowards(bossTrans.position, destination, moveSpeed * Time.deltaTime);

            if (dir != Vector3.zero)
            {
                bossTrans.rotation = Quaternion.RotateTowards(bossTrans.rotation,
                Quaternion.LookRotation(dir), 460 * Time.deltaTime);
            }
        }

        // 공격이 판정됬을때
        public bool MeleeAttackCheak()
        {
            Vector3 dir = DirectionFromPlayer();
            if (dir.sqrMagnitude < 4.0f * 4.0f)
            {
                transform.rotation = Quaternion.LookRotation(dir);
                return true;
            }

            return false;
        }

        // 캐스팅 여부
        public bool CastingSuccess()
        {
            castingGauge += Time.deltaTime;
            if (castingGauge >= castingRisesTime)
            {
                return true;
            }

            return false;
        }

        // 캐스팅 리셋
        public void ResetCastingGage()
        {
            castingGauge = 0;
        }
    }
}
