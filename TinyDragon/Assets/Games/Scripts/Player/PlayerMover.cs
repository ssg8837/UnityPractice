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
        ///플레이어의 점프 정도
        ///</summary>
        [Tooltip("레이어의 점프 정도")]
        [SerializeField]  public float jumpPower = 50f;

        private Rigidbody playerRigidbody;

        public Rigidbody Rigidbody
        {
            set
            {
                playerRigidbody = value;
            }
        }

        public bool Jump(Animator animator)
        {
            float inputY = Input.GetAxisRaw("Jump");
            if (inputY == 1)
            {
                animator.SetTrigger("Jump");
                playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                return true;
            }
            return false;
        }

        ///<summary>
        ///플레이어 이동 메소드
        ///</summary>
        ///<param name = "inputX">키보드 입력 X좌표</param>
        ///<param name = "inputZ">키보드 입력 Z좌표</param>
        public void Move(float inputX, float inputZ , Animator animator)
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
                Rotate(velocity);
            }
            velocity *= speed;

            animator.SetFloat("playerSpeed", velocity.magnitude);

            playerRigidbody.velocity = velocity;
       
        }

        ///<summary>
        ///플레이어에 맞춰 회전
        ///</summary>
        public void Rotate(Vector3 velocity)
        {
            Quaternion rotation = Quaternion.LookRotation(velocity);
            playerRigidbody.rotation = Quaternion.Slerp(playerRigidbody.rotation, rotation, speed/20 );
        }
    }

}
