using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    ///<summary>
    ///AI컨트롤러
    ///</summary>
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        private Mover aiMover;

        private Fighter aiFighter;
        private void Start()
        {
            aiMover = gameObject.GetComponent<Mover>();
            aiFighter = gameObject.GetComponent<Fighter>();
        }
        private void Update()
        {
            Transform playerTransform = GameObject.FindWithTag("Player").transform;
            if(DistanceToPlayer(playerTransform.position) < chaseDistance)
            {
                aiFighter.AttackTarget(playerTransform);
            }
            else
            {
                aiFighter.StopAttack();
            }
        }

        private float DistanceToPlayer(Vector3 playerVector)
        {
            return Vector3.Distance( playerVector, transform.position);
        }



    }
}
