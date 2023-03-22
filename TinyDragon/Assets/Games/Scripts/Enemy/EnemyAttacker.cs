using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyDragon.Core;

namespace TinyDragon.Enemy
{
    ///<summary>
    ///플레이어 이동 관련 클래스
    ///</summary>
    public class EnemyAttacker : MonoBehaviour, IAttacker
    {


        [Tooltip("공격 선딜")]
        [SerializeField] public float delayBefore = 0.8f;
        [Tooltip("공격 후딜")]
        [SerializeField] public float delayAfter = 0.8f;
        private Rigidbody playerRigidbody;

        private Animator enemyAnimator;


        [Tooltip("사정거리")]
        [SerializeField] public float attackDistance = 2f;

        [Tooltip("공격 사운드")]
        [SerializeField] private AudioSource enemyAttackSound;

        public Animator Animator
        {
            set
            {
                enemyAnimator = value;
            }
        }

        public float Distance
        {
            get
            {
                return attackDistance;
            }
        }

        public float jumpPower = 50f;
        public bool Attack(bool mJumping, Animator animator)
        {
            if (mJumping)
            {
                //TODO:점프 중 전투조작 처리
            }
            else
            {
                // Vector3 velocity = transform.TransformDirection(Vector3.forward);
                // velocity *= 2;
                StartCoroutine(AttackDelay(delayBefore));
                enemyAnimator.SetInteger("AttackAnim", Random.Range(1, 4));

                return true;

            }
            return false;
        }

        IEnumerator AttackDelay(float DelayTime)
        {
            enemyAnimator.SetBool("Delay", true);
            
            yield return new WaitForSeconds(DelayTime); 

            enemyAnimator.SetBool("Delay", false);
        }

        public void EnemyAttack()
        {
            enemyAttackSound.Play();
            enemyAnimator.SetInteger("AttackAnim", 0);
            StartCoroutine(AttackDelay(delayAfter));
        }

        public float getInterval()
        {
            return delayBefore;
        }
    }
}
