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

        ///<summary>
        ///플레이어의 위치 이동을 기억하기 시작한 타이머
        ///</summary>
        private float mFloatRemberTime = 0;


        ///<summary>
        ///플레이어가 어디 있는지 찾는 걸 시작한 타이머
        ///</summary>
        private float mFloatFindTime = 0;

        ///<summary>
        ///플레이어의 위치 이동을 기억하는 가?
        ///</summary>
        private bool mRemember = false;

        ///<summary>
        ///플레이어가 어디 있는지 찾는 중인 가?
        ///</summary>
        private bool mFind = false;

        private Vector3 lastPosVector;

        ///<summary>
        ///코루틴 실행 주기
        ///</summary>
        private float mCoroutineInterval = .5f;

        private Animator enemyAnimator;
        private EnemyMover mover;
        private EnemyAttacker attacker;

        private NavMeshAgent enemyNavMeshAgent;

        private Vector3 randomVector;

        /// <summary>
        ///피격 이펙트 색 변경 플래그
        /// </summary>
        private int mIntDamagedFlg;

        private Renderer[] renderers;

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

            mIntDamagedFlg = 0;

            renderers = transform.GetComponentsInChildren<Renderer>();

            StartCoroutine(SearchAndAttack(mCoroutineInterval)); //순찰 중(생략 가능)에 플레이어를 찾을 경우 쫓아가서 공격 코루틴
        }


        ///<summary>
        /// 순찰 중(생략 가능)에 플레이어를 찾을 경우 쫓아가서 공격 코루틴
        ///</summary>
        IEnumerator SearchAndAttack(float delay)
        {
            bool boolPlayerInEnemySight;
            while (true)
            {
                yield return new WaitForSeconds(delay);

                foreach (Renderer re in renderers)
                {

                    if (mIntDamagedFlg > 0)
                    {
                        re.material.color = Color.red;
                    }
                    else
                    {
                        re.material.color = Color.white;
                    }
                }
                Patrol();
                boolPlayerInEnemySight = FindTargets();
                if (mBoolStalk)
                {
                    if (!boolPlayerInEnemySight)
                    {
                        //INTERVAL 이용해서 플레이어에게 회전(일정 시간)
                        if (IntervalCheckForEnemy(ref mFloatRemberTime, mover.RememberTime, mRemember))
                        {
                            if (mFloatRemberTime == mCoroutineInterval)
                            {
                                lastPosVector = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
                            }
                            //TODO :: INTERVAL 이용해서 플레이어에게 회전(일정 시간)
                            mover.Rotate(lastPosVector, mFloatRemberTime, mCoroutineInterval, 0);
                        }
                        //일정시간 + X초간(회전 이후 X초), 랜덤한 방향으로 회전
                        else if (IntervalCheckForEnemy(ref mFloatFindTime, mover.FindTime, mFind))
                        {
                            if (mFloatFindTime == mCoroutineInterval)
                            {
                                randomVector = Vector3.zero;
                                randomVector.x += Random.Range(-1, 1);
                                randomVector.z += Random.Range(-1, 1);
                            }
                            //랜덤한 방향으로 회전 
                            mover.Rotate(randomVector, mFloatFindTime, mCoroutineInterval, 1);
                        }
                        //TODO :: 일정 시간 지나면 추적 플래그 종료 후 순찰로 돌아감,
                        else
                        {
                            Debug.Log("추적 플래그 종료 ");
                            mBoolStalk = false;
                        }

                    }
                }
                mover.UpdateMoveMotion();
            }
        }

        ///<summary>
        /// 적의 시야에서 플레이어를 찾을 것
        ///</summary>
        private bool FindTargets()
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
                        return true;
                    }
                }
            }
            return false;

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
            mFloatRemberTime = 0;
            mFloatFindTime = 0;
            mRemember = true;
            mFind = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerWeapon"))
            {
                Attacked(5, 1, Vector3.zero);
            }
            else if (other.gameObject.transform.Equals(mListPatrol[mIntPatrolCounter]))
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


        private bool IntervalCheckForEnemy(ref float timeChecker, float interval, bool waitFlg)
        {
            if (waitFlg)
            {
                timeChecker += mCoroutineInterval;
                if (timeChecker >= interval)
                {
                    waitFlg = false;
                }
            }

            return waitFlg;
        }

        public void Attacked(float damage, float velocitypower, Vector3 velocity)
        {
            enemyAnimator.SetTrigger("Damage");

            StartCoroutine("damagedColor");
        }

        IEnumerator damagedColor()
        {
            mIntDamagedFlg++;

            yield return new WaitForSeconds(1f);

            mIntDamagedFlg--;
        }

    }
}