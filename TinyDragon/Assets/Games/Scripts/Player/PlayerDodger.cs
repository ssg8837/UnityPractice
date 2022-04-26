using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyDragon.Core;
using System;

namespace TinyDragon.Player
{
    ///<summary>
    ///플레이어 이동 관련 클래스
    ///</summary>
    public class PlayerDodger : MonoBehaviour, IDodger
    {
        ///<summary>
        ///플레이어의 닷지 스피드
        ///</summary>
        [Tooltip("플레이어의 닷지 스피드")]
        [SerializeField] private float stepSpeed = 8f;
        
        [Tooltip("닷지 후딜")]
        [SerializeField] private float dodgeInterval = 1f;
        
        public bool Dodge(float inputX, float inputZ, Animator animator , Rigidbody rigidbody)
        {
            animator.SetTrigger("Dodge");

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

            animator.SetFloat("playerSpeed", 0);

            rigidbody.velocity = velocity;


            return true;
        }

        public float getInterval()
        {
            return dodgeInterval;
        }
    }
}
