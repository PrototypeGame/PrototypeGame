using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace Boss
{
    public class BossPattern
    {
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

        public int phase = 1;

        public float excuteSkillTiem = 1.5f;
        public float idleWaitTime = 1.5f;
        public float castingRisesTime = 3.0f;
        public float attackCastingRises = 0.1f;
        public Transform[] playerTransforms; // 유저들의 위치
        public BossPattern[] patterns;
        public UnityEngine.Events.UnityEvent camShake;


        private float castingGauge = 0.0f;
        private Transform closePlayerTrans; // 가까운 유저의 위치
        private Dictionary<GolemSkill, AttackActionBase> skills;
        private Transform bossTrans;
        private Animator anim;
        private Root ai;

        private void Awake()
        {
            bossTrans = GetComponent<Transform>();
            anim = GetComponent<Animator>();

            MovingObjectSMB<GolemBehavior>.Initialize(anim, this);

            //skills = new Dictionary<GolemSkill, AttackActionBase>();
            //skills.Add(GolemSkill.RockfallRain, GetComponent<RockfallRain>());

            #region Pattern
            patterns = new BossPattern[3];
            BossPattern phase = new BossPattern();

            phase.weight = new int[] { 3, 5, 8, 10 };
            phase.skill = new AttackActionBase[4];
            phase.skill[0] = (GetComponentInChildren<Earthquake>());
            phase.skill[1] = (GetComponentInChildren<RushAttack>());
            phase.skill[2] = (GetComponentInChildren<RockDrop>());
            phase.skill[3] = (GetComponentInChildren<ShockWave>());
            patterns[0] = phase;

            phase = new BossPattern();
            phase.weight = new int[] { 2, 4, 7, 10 };
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

            ai = new Root();
            ai.AddBranch(
                new Action(ClosePlayerResearch),
                new Repeat(patterns.Length).AddBranch(

                    new While(NotDead).AddBranch(
                        new ConditionalBranch(CastingSuccess).AddBranch(
                            new SetBool(anim, "Run", false),
                            new Wait(excuteSkillTiem),
                            new SetTrigger(anim, "Skill"),
                            new Wait(1.5f),
                            new Action(ExcuteSkill),
                            new WaitAnimationAnd(anim, "Idle"),
                            new Wait(idleWaitTime),
                            new Action(ResetCastingGage)
                            ),

                        new SetBool(anim, "Run"),

                        new ConditionalBranch(MeleeAttack).AddBranch(
                            new SetBool(anim, "Run", false),
                            new SetTrigger(anim, "MeleeAttack"),
                            new WaitAnimationAnd(anim, "Idle"),
                            new Wait(idleWaitTime),
                            new Action(ClosePlayerResearch)
                        )
                    )
                ),
                new SetTrigger(anim, "Dead"),
                new Abort()
            );
        }

        private void Update()
        {
            ai.Tick();
            CastingSuccess();
        }

        public bool NotDead()
        {
            return true;
        }

        public void ExcuteSkill()
        {
            switch (phase)
            {
                case 1:
                    RandomSkillSelect(patterns[phase - 1]);
                    break;
                case 2:
                    RandomSkillSelect(patterns[phase - 1]);
                    break;
                case 3:
                    RandomSkillSelect(patterns[phase - 1]);
                    break;
            }
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

        public void TracingClosePlayer()
        {
            Vector3 destination = closePlayerTrans.position;
            destination.y = bossTrans.position.y;

            Vector3 dir = closePlayerTrans.position - bossTrans.position;
            dir.y = 0.0f;

            bossTrans.position = Vector3.MoveTowards(bossTrans.position, destination, moveSpeed * Time.deltaTime);

            if (dir != Vector3.zero)
            {
                bossTrans.rotation = Quaternion.RotateTowards(bossTrans.rotation,
                Quaternion.LookRotation(dir), 460 * Time.deltaTime);
            }
        }

        // 공격이 판정됬을때
        public bool MeleeAttack()
        {
            Vector3 dir = closePlayerTrans.position - bossTrans.position;

            if (dir.sqrMagnitude < 4.0f * 4.0f)
            {
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

        public void ResetCastingGage()
        {
            castingGauge = 0;
        }
    }
}
