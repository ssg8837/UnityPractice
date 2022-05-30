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
        [SerializeField] private float speed = 3.5f;

        ///<summary>
        ///적의 점프 정도
        ///</summary>
        [Tooltip("적의 점프 정도")]
        [SerializeField] private float jumpPower = 50f;

        [SerializeField] private float findPlayerTime = 5f;

        [SerializeField] private float remmberPlayerTime = 1f;


        ///<summary>
        ///회전을 얼마나 몇 초 간격으로 시킬것인가?
        ///</summary>
        [Tooltip("회전을 얼마나 몇 초 간격으로 시킬것인가?")]
        [SerializeField] private float rotationDelay = 0.05f;

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

        public float FindTime
        {
            get
            {
                return findPlayerTime;
            }
        }

        public float RememberTime
        {
            get
            {
                return remmberPlayerTime;
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
            navMesh.speed = speed;
        }

        public void UpdateMoveMotion()
        {
            EnemyAnimator.SetFloat("MoveSpeed", navMesh.speed);
        }

        ///<summary>
        ///플레이어에 맞춰 회전
        ///</summary>
        public void Rotate(Vector3 velocity, float targetTime, float controllerInterval, int intFlg)
        {
            if (velocity == Vector3.zero)
            {
                velocity = transform.TransformDirection(Vector3.forward);
            }

            navMesh.speed = 2f;
            float rotateTimer = 0f;
            switch (intFlg)
            {
                case 0:
                    rotateTimer = remmberPlayerTime;
                    break;
                case 1:
                    rotateTimer = findPlayerTime;
                    break;
            }
            StartCoroutine(RotateCorutine(velocity, targetTime - controllerInterval, controllerInterval, rotateTimer));
        }

        IEnumerator RotateCorutine(Vector3 dir, float oldTargetTime, float coroutineInterval, float rotateTimer)
        {
            for (float i = 0; i < coroutineInterval; i += rotationDelay)
            {
                yield return new WaitForSeconds(rotationDelay);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), rotationDelay);
            }
        }

        public void Stop()
        {
            navMesh.isStopped = true;
        }
    }

}
