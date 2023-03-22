using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyDragon.Core;

namespace TinyDragon.Player
{
    ///<summary>
    ///플레이어 이동 관련 클래스
    ///</summary>
    public class PlayerAttacker : MonoBehaviour, IAttacker
    {


        [Tooltip("연속공격 후딜")]
        [SerializeField] public float attackInterval = 0.8f;
        private Rigidbody playerRigidbody;


        [Tooltip("사정거리")]
        [SerializeField] public float attackDistance = 0.8f;

        public Rigidbody Rigidbody
        {
            set
            {
                playerRigidbody = value;
            }
        }

        ///<summary>
        ///공격 후 공격불가 플래그
        ///</summary>
        private bool mAttackWait = false;


        [Tooltip("공격 이펙트")]
        [SerializeField]
        private ParticleSystem[] playerAttackParticle;


        [Tooltip("공격 충돌판정")]
        [SerializeField]
        private GameObject[] playerAttackChecker;


        [Tooltip("공격 사운드")]
        [SerializeField]
        private AudioSource[] playerAttackSound;

        public bool Attack(bool mJumping, Animator animator)
        {
            if (mJumping)
            {
                //TODO:점프 중 전투조작 처리
            }
            else
            {
                Vector3 velocity = transform.TransformDirection(Vector3.forward);
                velocity *= 2;
                animator.SetTrigger("Attack");

                playerRigidbody.velocity = velocity;

                return true;

            }
            return false;
        }

        public bool PlayerMeleeAttack(int aComboAttack, Animator animator)
        {
            mAttackWait = false;
            ParticleSystem particle = playerAttackParticle[aComboAttack - 1];
            AudioSource audioSource = playerAttackSound[aComboAttack - 1];
            playerAttackChecker[aComboAttack - 1].SetActive(true);
            if (!particle.isPlaying)
            {
                audioSource.Play();
                particle.Play();
                switch (aComboAttack)
                {
                    case 3:
                        mAttackWait = true;
                        break;
                    default:
                        break;
                }
            }
            return mAttackWait;
        }

        public void StopAttackParticle()
        {
            foreach (ParticleSystem particle in playerAttackParticle)
            {
                if (particle.isPlaying)
                {
                    particle.Stop();
                }
            }
            foreach (AudioSource audioSource in playerAttackSound)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
            foreach (GameObject attackChecker in playerAttackChecker)
            {
                attackChecker.SetActive(false);
            }
        }

        public float getInterval()
        {
            return attackInterval;
        }
    }
}
