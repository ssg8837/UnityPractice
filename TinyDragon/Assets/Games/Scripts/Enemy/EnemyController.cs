using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TinyDragon.Core;
using TinyDragon.Enemy;

namespace TinyDragon.Enemy
{
    public class EnemyController : MonoBehaviour
    {

        [SerializeField]
        public float viewRadius;
        [Range(0, 360)]

        [SerializeField]
        public float viewAngle;

        [SerializeField]
        public LayerMask targetMask;

        ///<summary>
        ///순찰 플래그
        ///</summary>
        [Tooltip("순찰 플래그")]
        [SerializeField] private bool mBoolPatrol = false;

        ///<summary>
        ///순찰 지점 리스트
        ///</summary>
        [Tooltip("순찰 지점 리스트")]
        [SerializeField] private List<Transform> mListPatrol;


        ///<summary>
        ///순찰 지점 최댓수
        ///</summary>
        private int mIntPatrolMax;
        ///<summary>
        ///현재 순찰 지점 번호
        ///</summary>
        private int mIntPatrolCounter;

        ///<summary>
        ///지금 점핑중인가?
        ///</summary>
        private bool mBoolJumping = false;

        ///<summary>
        ///지금 쫓아가는 중인가
        ///</summary>
        private bool mBoolStalk = false;

        ///<summary>
        ///역으로 순찰을 해야 될 지점인가? 
        ///</summary>
        private bool mBoolPatrolReverse = false;


        private Animator enemyAnimator;
        private EnemyMover mover;
        private EnemyAttacker attacker;

        private NavMeshAgent enemyNavMeshAgent;


        private void Start()
        {
            enemyAnimator = GetComponent<Animator>();

            enemyNavMeshAgent = GetComponent<NavMeshAgent>();

            mover = GetComponent<EnemyMover>();

            mover.NavMesh = enemyNavMeshAgent;

            mover.Animator = enemyAnimator;


            attacker = GetComponent<EnemyAttacker>();

            attacker.Animator = enemyAnimator;


            mIntPatrolCounter = 0;

            mIntPatrolMax = mListPatrol.Count;

            StartCoroutine("SearchAndAttack", .5f); //순찰 중(생략 가능)에 플레이어를 찾을 경우 쫓아가서 공격 코루틴
        }


        ///<summary>
        /// 순찰 중(생략 가능)에 플레이어를 찾을 경우 쫓아가서 공격 코루틴
        ///</summary>
        IEnumerator SearchAndAttack(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);

                Patrol();
                FindTargets();
                if (mBoolStalk)
                {
                    //TODO :: INTERVAL 이용해서 플레이어에게 회전(일정 시간)

                    //TODO :: 일정시간 + 3초간(회전 이후 3초), 랜덤한 방향으로 회전 

                    //TODO :: 일정 시간 지나면 추적 플래그 종료 후 순찰로 돌아감,

                    mBoolStalk = false;
                }
                mover.UpdateMoveMotion();
            }
        }

        ///<summary>
        /// 적의 시야에서 플레이어를 찾을 것
        ///</summary>
        private void FindTargets()
        {
            Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            for (int i = 0; i < targetInViewRadius.Length; i++)
            {
                Transform target = targetInViewRadius[i].transform;

                Vector3 dirToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (Physics.Raycast(transform.position, dirToTarget, dstToTarget, targetMask))
                    {
                        InteractPlayer(target);
                        return;
                    }
                }
            }

        }

        ///<summary>
        /// 적이 순찰하는 처리
        ///</summary>
        private void Patrol()
        {
            if (!mBoolStalk && !mBoolJumping && mIntPatrolMax > 0)
            {
                mover.Move(mListPatrol[mIntPatrolCounter]);
            }
        }

        ///<summary>
        /// 플레이어에게 반응하는 처리
        ///</summary>
        private void InteractPlayer(Transform target)
        {
            if (!mBoolJumping && !mBoolStalk)
            {
                mover.Stop();
                mover.Jump();
                mBoolJumping = true;
            }
            else if (mBoolStalk)
            {
                if (Vector3.Distance(target.position, transform.position) <= attacker.Distance)
                {
                    mover.Stop();
                    attacker.Attack(mBoolJumping, enemyAnimator);
                }
                else
                {
                    mover.Move(target);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.transform.Equals(mListPatrol[mIntPatrolCounter]))
            {
                if (mBoolPatrolReverse)
                {
                    if (mIntPatrolCounter == 0)
                    {
                        mBoolPatrolReverse = false;
                        mIntPatrolMax++;
                    }
                    else
                    {
                        mIntPatrolCounter--;
                    }
                }
                else
                {
                    if (mIntPatrolCounter == mIntPatrolMax - 1)
                    {
                        mBoolPatrolReverse = false;
                        mIntPatrolCounter--;
                    }
                    else
                    {
                        mIntPatrolCounter++;
                    }
                }

            }
        }

        private void JumpingEnd()
        {
            mBoolJumping = false;
            mBoolStalk = true;
        }

    }
}