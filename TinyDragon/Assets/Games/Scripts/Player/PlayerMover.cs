using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyDragon.Core;

namespace TinyDragon.Player
{
    ///<summary>
    ///플레이어 이동 관련 클래스
    ///</summary>
    public class PlayerMover : MonoBehaviour
    {

        ///<summary>
        ///플레이어의 스피드
        ///</summary>
        [Tooltip("플레이어의 스피드")]
        [SerializeField] private float speed = 5f;
        

        ///<summary>
        ///플레이어의 닷지 스피드
        ///</summary>
        [Tooltip("플레이어의 닷지 스피드")]
        private float stepSpeed = 8f;

        ///<summary>
        ///플레이어 리지드 바디
        ///</summary>
        private Rigidbody playerRigidbody;

        ///<summary>
        ///점프 중 플래그
        ///</summary>
        private bool mJumping = false;

        ///<summary>
        ///닷지 중 플래그
        ///</summary>
        private bool mDodging = false;

        ///<summary>
        ///공격 중 플래그
        ///</summary>
        private bool mAttacking = false;

        ///<summary>
        ///현재 공격 중인 단계
        ///</summary>
        private uint mMeleeComboStage = 0;

        private Animator playerAnimator;

        public float jumpPower = 50f;

        // Start is called before the first frame update
        void Start()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            playerAnimator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");

            float inputDodge = Input.GetAxisRaw("Dodge");
            float inputAttack = Input.GetAxisRaw("Attack");

            
            if(mDodging){}                  //TODO: 회피중 조작 처리(미정)
            else                            //회피중이 아닐 때 처리(평시 처리)
            {
                if(!mJumping && inputDodge == Constants.INPUT_ON)    //점프중이 아닐때 닷지키가 눌림.
                {
                    Dodge(inputX, inputZ);
                }
                else if(inputAttack == Constants.INPUT_ON)
                {
                    if(mJumping)
                    {
                        //TODO:점프 중 전투조작 처리
                    }
                    else 
                    {   
                        //TODO:기본 공격 처리
                        if(mMeleeComboStage < 3)
                        {
                            mMeleeComboStage++;

                            Vector3 velocity = transform.TransformDirection(Vector3.forward);
                            velocity *= 2;
                            playerAnimator.SetFloat("ComboStage", mMeleeComboStage);
                            Debug.Log(mMeleeComboStage);
                            
                            playerRigidbody.velocity = velocity;

                        }

                    }
                }
                else
                {
                    Move(inputX, inputZ);

                    if(!mJumping)
                    {
                        Jump();
                    }
                }

            }
        }

        private void Dodge(float inputX, float inputZ)
        {
            mDodging = true;
            playerAnimator.SetTrigger("Dodge");

            Vector3 velocity;

            if (inputX != Constants.INPUT_OFF || inputZ != Constants.INPUT_OFF) //방향이랑 눌렸을땐 방향키 방향으로 회피
            {
                velocity = new Vector3(inputX, 0, inputZ);
                velocity = velocity.normalized;

            }
            else
            {
                velocity = transform.TransformDirection(Vector3.back);
            }

            velocity *= stepSpeed;

            playerAnimator.SetFloat("playerSpeed", 0);

            playerRigidbody.velocity = velocity;
        }

        private void Jump()
        {
            float inputY = Input.GetAxisRaw("Jump");
            if (inputY == 1)
            {
                playerAnimator.SetTrigger("Jump");
                playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                mJumping = true;
            }
        }

        private void Move(float inputX, float inputZ)
        {
            Vector3 velocity = new Vector3(inputX, 0, inputZ);

            if (velocity.magnitude > 1)
            {
                velocity = velocity.normalized;
            }
            //이동중일 경우
            if( inputX != 0 || inputZ != 0)
            {
                //이동 방향에 맞춰 회전
                playerRotation(velocity);
            }
            velocity *= speed;

            playerAnimator.SetFloat("playerSpeed", velocity.magnitude);

            playerRigidbody.velocity = velocity;
       
        }

        ///<summary>
        ///플레이어에 맞춰 회전
        ///</summary>
        private void playerRotation(Vector3 velocity)
        {
            Quaternion rotation = Quaternion.LookRotation(velocity);
            playerRigidbody.rotation = Quaternion.Slerp(playerRigidbody.rotation, rotation, speed/20 );
        }


        public void InitJumping()
        {
            mJumping = false;
        }

        public void InitDodging()
        {
            mDodging = false;
            Debug.Log(mDodging);
        }
    }
}
