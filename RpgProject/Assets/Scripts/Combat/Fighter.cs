using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    /// <summary>
    /// 플레이어가 근접 전투원일 경우 전투 처리 클래스
    /// </summary>
    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] float weaponRange =2f;
        Transform target;

        Mover playerMover;

        private void Start()
        {
            playerMover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (target != null)
            {
                if(Vector3.Distance(transform.position, target.position)    //플레이어와 타겟간 거리
                    > weaponRange)                                          //무기 길이 보다 길때
                {
                    playerMover.MoveTo(target.position);                    //이동 메소드
                }
                else
                {
                    ActionScheduler.StartAction(this);
                }
            }
        }
        public void AttackEnemey(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }
        
        public void Stop()
        {
            Debug.Log("Fighter Stop");
        }

        public void StopAttack()
        {
            target = null;
        }
    }
}