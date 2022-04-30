using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TinyDragon.Core;
using System;

namespace TinyDragon.Enemy
{
    ///<summary>
    ///적 이동 관련 클래스
    ///</summary>
    public class EnemyMover : MonoBehaviour
    {

        ///<summary>
        ///적의 스피드
        ///</summary>
        [Tooltip("적의 스피드")]
        [SerializeField] private float speed = 5f;

        ///<summary>
        ///적의 점프 정도
        ///</summary>
        [Tooltip("적의 점프 정도")]
        [SerializeField] public float jumpPower = 50f;


        private Rigidbody EnemyRigidbody;
        private Animator EnemyAnimator;

        private NavMeshAgent navMesh;

        public NavMeshAgent NavMesh
        {
            set
            {
                navMesh = value;
            }
        }

        public Rigidbody Rigidbody
        {
            set
            {
                EnemyRigidbody = value;
            }
        }

        public Animator Animator
        {
            set
            {
                EnemyAnimator = value;
            }
        }

        public void Jump()
        {
            EnemyAnimator.SetTrigger("Jump");
        }

        ///<summary>
        ///적 이동 메소드
        ///</summary>
        public void Move(Transform transform)
        {
            navMesh.destination = transform.position;
            navMesh.isStopped = false;
        }

        public void UpdateMoveMotion()
        {
            EnemyAnimator.SetFloat("MoveSpeed", navMesh.speed);
        }

        ///<summary>
        ///플레이어에 맞춰 회전
        ///</summary>
        public void Rotate(Vector3 velocity)
        {
        }

        public void Stop()
        {
            navMesh.isStopped = true;
        }
    }

}
