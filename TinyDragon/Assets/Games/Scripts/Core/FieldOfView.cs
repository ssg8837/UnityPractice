using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyDragon.Core
{
    public class FieldOfView : MonoBehaviour
    {
        
        [SerializeField]
        public float viewRadius;
        [Range(0, 360)]
        
        [SerializeField]
        public float viewAngle;

        [SerializeField]
        public LayerMask targetMask;
        

        public List<Transform> visibleTargets = new List<Transform>();

        // Start is called before the first frame update
        void Start()
        {
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

        void FindTargets()
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
                        print("raycast hit!"); 
                        Debug.DrawRay(transform.position, dirToTarget * 10f, Color.red, 5f); 
                        
                        GetComponentInChildren<Animator>().SetTrigger("Check");
                    }
                }
            }
        }
    }
}
