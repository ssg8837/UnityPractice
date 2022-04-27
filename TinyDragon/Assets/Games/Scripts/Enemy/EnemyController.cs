using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TinyDragon.Core;
using TinyDragon.Enemy;

namespace TinyDragon.Enemy
{
    public class EnemyController : MonoBehaviour
    {

        [SerializeField]
        public float viewRadius;
        [Range(0, 360)]
        
        [SerializeField]
        public float viewAngle;

        [SerializeField]
        public LayerMask targetMask;
        
        private bool mBoolJump = false;

        public List<Transform> visibleTargets = new List<Transform>();

        private Animator EnemyAnimator;
        private EnemyMover mover;

        private void Start() 
        {
            EnemyAnimator =  GetComponent<Animator>();

            mover = GetComponent<EnemyMover>();

            mover.NavMesh = GetComponent<NavMeshAgent>();

            
            StartCoroutine("FindTargetDelay",.5f);
        }

        
        IEnumerator FindTargetDelay(float delay)
        {
            while(true)
            {
                yield return new WaitForSeconds(delay);
                FindTargets();
            }
        }

        private void FindTargets()
        {
            Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            for(int i = 0; i< targetInViewRadius.Length; i++)
            {
                Transform target = targetInViewRadius[i].transform;

                Vector3 dirToTarget = (target.position - transform.position).normalized;

                if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle/2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if(Physics.Raycast(transform.position, dirToTarget, dstToTarget, targetMask))
                    {
                        visibleTargets.Add(target); 
                        
                        if(!mBoolJump)
                        {
                            EnemyAnimator.SetTrigger("Check");
                            mBoolJump = true;
                        }
                        else
                        {
                            mover.Move(target, EnemyAnimator);
                        }
                        return;
                        
                    }
                }
            }
            mover.Stop(EnemyAnimator);
        }
    }
}