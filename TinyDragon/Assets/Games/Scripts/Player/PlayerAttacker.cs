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

        
        ///<summary>
        ///공격 후 공격불가 타이머
        ///</summary>
        private float mAttackWaitTimer = 0;


        ///<summary>
        ///현재 공격 중인 단계
        ///</summary>
        private uint mMeleeComboStage = 0;


        public float jumpPower = 50f;
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
            switch(aComboAttack)
            {
                case 3:
                    animator.ResetTrigger("Attack");
                    mAttackWait = true;
                    mAttackWaitTimer = 0;
                    return true;
                default:
                    break;
            }
            return false;
        }

        public float getInterval()
        {
            return attackInterval;
        }
    }
}
