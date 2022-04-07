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
        [SerializeField] float timeBetweenAttack = 0.5f;
        Transform target;

        ///<summary>
        ///데미지(임시_무기 없을시 )
        ///</summary>
        float damage = 5f;

        Mover playerMover;
        float TimeSinceLastAttack = 0;

        private void Start()
        {
            playerMover = GetComponent<Mover>();
        }

        private void Update()
        {
            TimeSinceLastAttack += Time.deltaTime;
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
                    AttackBehaviour();
                }
            }
        }

        private void AttackBehaviour()
        {
            if(TimeSinceLastAttack > timeBetweenAttack)
            {
                GetComponent<Animator>().SetTrigger("triggerAttack");
                TimeSinceLastAttack = 0;
            }

        }

        public void AttackEnemey(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            if(target.GetComponent<Health>().IsDead())
            {
                target = null;
            }
        }
        
        public void Stop()
        {
            Debug.Log("Fighter Stop");
        }

        public void StopAttack()
        {
            target = null;
            gameObject.GetComponent<Animator>().SetTrigger("tiggerStopAttack");
        }

        ///<summary>
        ///공격 애니메이션 재생시 이벤트
        ///</summary>
        void Hit()
        {
            if(target != null)
            {
                target.GetComponent<Health>().TakeDamage(damage);
                if(target.GetComponent<Health>().IsDead())
                {
                    target = null;
                }
            }
        }
    }
}
