using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement 
{
    ///<summary>
    /// 이동 처리 클래스
    ///</summary>
    public class Mover : MonoBehaviour, IAction
    {
        // 길을 찾아서 이동할 에이전트
        private NavMeshAgent navAgent;

        // 이동 애니메이션을 재생할 애니메이터
        private Animator moveAnimator;

        private void Awake() 
        {
            // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장
            navAgent = GetComponent<NavMeshAgent>();
            // 게임이 시작되면 게임 오브젝트에 부착된 Animator 컴포넌트를 가져와서 저장
            moveAnimator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            // if(Input.GetMouseButton(0))
            // {
            //     MoveToCursor();
            // }
            UpdateAnimator();

        }

        // private void MoveToCursor()
        // {
        //     //마우스 포지션으로 레이캐스트
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     //레이캐스트에 맞은 정보
        //     RaycastHit hit;
        //     // 맞았는가 판정하면서 hit에 정보 저장.
        //     bool hasHit = Physics.Raycast(ray, out hit);

        //     //레이캐스트에 맞은 정보가 있을 경우
        //     if(hasHit)
        //     {
        //         MoveTo(hit.point);
        //     }
        // }

        ///<summary>
        /// 목적지 까지 이동
        ///</summary>
        public void MoveTo(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            // 에이전트에게 목적지를 알려주는 함수        
            navAgent.SetDestination(destination);
            navAgent.isStopped = false;
        }
        ///<summary>
        /// 이동을 멈춤
        ///</summary>
        public void Stop()
        {
            navAgent.isStopped = true;
        }

        
        ///<summary>
        /// 이동 애니메이션을 갱신함.
        ///</summary>
        private void UpdateAnimator()
        {
            //에이전트의 현재속력
            Vector3 velocity = navAgent.velocity;

            //에이전트의 현재속력을 로컬 좌표 기준으로 변환
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            //애니메이션을 로컬z축의 현재속력에 맞춰서 재생함.
            moveAnimator.SetFloat("forwardSpeed", localVelocity.z);
        }
    }

}
