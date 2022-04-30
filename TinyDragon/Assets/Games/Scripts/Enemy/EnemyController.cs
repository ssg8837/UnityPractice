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

        public List<Transform> visibleTargets = new List<Transform>();

        private Animator EnemyAnimator;
        private EnemyMover mover;
        private EnemyAttacker attacker;

        private void Start()
        {
            EnemyAnimator = GetComponent<Animator>();

            mover = GetComponent<EnemyMover>();

            mover.NavMesh = GetComponent<NavMeshAgent>();

            mover.Animator = EnemyAnimator;


            attacker = GetComponent<EnemyAttacker>();

            attacker.Animator = EnemyAnimator;


            mIntPatrolCounter = 0;

            mIntPatrolMax = mListPatrol.Count;

            StartCoroutine("SearchAndAttack", .5f);
        }


        IEnumerator SearchAndAttack(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindTargets();
                mover.UpdateMoveMotion();
            }
        }

        private void FindTargets()
        {
            if (!mBoolStalk && mIntPatrolMax > 0)
            {
                mover.Move(mListPatrol[mIntPatrolCounter]);
            }

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
                        visibleTargets.Add(target);

                        if (!mBoolJumping && !mBoolStalk)
                        {
                            mover.Stop();
                            mover.Jump();
                            mBoolJumping = true;
                        }
                        else if (mBoolStalk)
                        {
                            if (Vector3.Distance(transform.position, target.transform.position) < 5f) //remainingDistancePermalink로 수정할것.
                            {
                                mover.Stop();
                                attacker.Attack(mBoolJumping, EnemyAnimator);
                            }
                            else
                            {
                                mover.Move(target);
                            }
                        }
                        return;

                    }
                }
            }
            mBoolStalk = false;
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