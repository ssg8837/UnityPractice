using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movements;
using RPG.Combat;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        private CombatTarget combatTarget;
        
        private Fighter playerFighter;

        private Mover playerMover; 
        private void Start()
        {
            playerMover = GetComponent<Mover>(); 
            playerFighter = GetComponent<Fighter>(); 
        }

        // Update is called once per frame
        private void Update()
        {
            //마우스 포지션으로 레이캐스트
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            InteractWithCombat(ray);
            InteractWithMovement(ray);
        }

        private void InteractWithCombat(Ray ray)
        {
            RaycastHit[] rayTargets = Physics.RaycastAll(ray);
            foreach(RaycastHit rayTarget in rayTargets)
            {
                CombatTarget target = rayTarget.transform.GetComponent<CombatTarget>();
                if(target != null &&
                   Input.GetMouseButton(0))
                   {
                       playerFighter.Attack();
                   }
            }          
        }
        private void InteractWithMovement(Ray ray)
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor(ray);
            }
        }

        private void MoveToCursor(Ray ray)
        {
            //레이캐스트에 맞은 정보
            RaycastHit hit;
            // 맞았는가 판정하면서 hit에 정보 저장.
            bool hasHit = Physics.Raycast(ray, out hit);

            //레이캐스트에 맞은 정보가 있을 경우
            if(hasHit)
            {
                playerMover.MoveTo(hit.point);
            }
        }

    }
}
