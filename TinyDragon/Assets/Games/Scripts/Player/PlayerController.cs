using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyDragon.Core;
using TinyDragon.Player;

namespace TinyDragon.Player
{
    public class PlayerController : MonoBehaviour
    {
        
        ///<summary>
        ///플레이어 리지드 바디
        ///</summary>
        private Rigidbody playerRigidbody;
        
        private Animator playerAnimator;

        private PlayerMover mover;
        private PlayerDodger dodger;
        private PlayerAttacker attacker;

        ///<summary>
        ///점프 중 플래그
        ///</summary>
        private bool mBoolJumping = false;

        ///<summary>
        ///닷지 중 플래그
        ///</summary>
        private bool mBoolDodging = false;

        ///<summary>
        ///공격 중 플래그
        ///</summary>
        private bool mBoolAttacking = false;

        ///<summary>
        ///닷지 후 닷지불가 타이머
        ///</summary>
        private float mDodgeWaitTimer = 0;

        ///<summary>
        ///공격 후 공격불가 타이머
        ///</summary>
        private float mAttackWaitTimer = 0;

        private bool mBoolWaitDodgeFlg = false;
        private bool mBoolWaitAttackFlg = false;

        void Start()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            playerAnimator = GetComponent<Animator>();

            mover = GetComponent<PlayerMover>();
            attacker = GetComponent<PlayerAttacker>();
            dodger = GetComponent<PlayerDodger>();
            
            mover.Rigidbody = playerRigidbody;
            attacker.Rigidbody = playerRigidbody;
            dodger.Rigidbody = playerRigidbody;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");

            float inputDodge = Input.GetAxisRaw("Dodge");
            float inputAttack = Input.GetAxisRaw("Attack");

            inputDodge = IntervalCheck(inputDodge , ref mDodgeWaitTimer, dodger.getInterval(), ref mBoolWaitDodgeFlg);
            inputAttack = IntervalCheck(inputAttack , ref mAttackWaitTimer, attacker.getInterval(), ref mBoolWaitAttackFlg);

            if (mBoolDodging) { }                  //TODO: 회피중 조작 처리(미정)
            else                            //회피중이 아닐 때 처리(평시 처리)
            {
                if (!mBoolJumping && inputDodge == Constants.INPUT_ON)    //점프중이 아닐때 닷지키가 눌림.
                {
                    mBoolDodging = dodger.Dodge(inputX, inputZ, playerAnimator);
                    if(mBoolDodging)
                    {
                        mBoolWaitDodgeFlg = mBoolDodging;
                        mDodgeWaitTimer = 0;
                    }
                }
                else if (inputAttack == Constants.INPUT_ON)
                {
                    attacker.Attack(mBoolJumping, playerAnimator);
                }
                else
                {
                    mover.Move(inputX, inputZ, playerAnimator);

                    if (!mBoolJumping)
                    {
                        mBoolJumping = mover.Jump(playerAnimator);
                    }
                }

            }
        }

        private float IntervalCheck(float input, ref float timeChecker, float interval, ref bool waitFlg)
        {
            if (waitFlg)
            {
                timeChecker += Time.deltaTime;
                if (timeChecker >= interval)
                {
                    Debug.Log(timeChecker);
                    waitFlg = false;
                }
                else
                {
                    input = Constants.INPUT_OFF;
                }
            }

            return input;
        }

        
        public void InitJumping()
        {
            mBoolJumping = false;
        }

        public void InitDodging()
        {
            mBoolDodging = false;
        }

        private void MeleeAttack(int aComboAttack)
        {
           mBoolWaitAttackFlg = attacker.PlayerMeleeAttack(aComboAttack, playerAnimator);
           mAttackWaitTimer = 0;
        }
        
    }
}