using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyDragon.Core;

namespace TinyDragon.Player
{
    ///<summary>
    ///플레이어 이동 관련 클래스
    ///</summary>
    public class PlayerMover : MonoBehaviour, IMover
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


        public bool Jump(Animator animator, Rigidbody rigidbody)
        {
            float inputY = Input.GetAxisRaw("Jump");
            if (inputY == 1)
            {
                animator.SetTrigger("Jump");
                rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                return true;
            }
            return false;
        }

        public void Move(float inputX, float inputZ , Animator animator, Rigidbody rigidbody)
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
                playerRotation(velocity, rigidbody);
            }
            velocity *= speed;

            animator.SetFloat("playerSpeed", velocity.magnitude);

            rigidbody.velocity = velocity;
       
        }

        ///<summary>
        ///플레이어에 맞춰 회전
        ///</summary>
        public void playerRotation(Vector3 velocity , Rigidbody rigidbody)
        {
            Quaternion rotation = Quaternion.LookRotation(velocity);
            rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, rotation, speed/20 );
        }
    }

}
