using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        private CombatTarget currentTarget;
        
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

            if(!InteractWithCombat(ray)) //적이 있을 경우 전투 처리로
            {
                if(InteractWithMovement(ray)) //적이 없을 경우 이동 처리로
                {
                    playerFighter.StopAttack();
                }
            }
        }

        /// <summary>
        /// 플레이어 컨트롤러 전투 처리 메소드
        /// </summary>
        /// <param name = "ray">레이</param>
        ///<returns> 전투처리 : TRUE, 이동처리 : FALSE </returns>
        private bool InteractWithCombat(Ray ray)
        {
            RaycastHit[] rayTargets = Physics.RaycastAll(ray);
            foreach(RaycastHit rayTarget in rayTargets)
            {
                CombatTarget target = rayTarget.transform.GetComponent<CombatTarget>();
                //마우스 위치에 적이 있을 경우
                if(target != null &&
                   Input.GetMouseButton(0)) //마우스 버튼이 눌린 경우
                   {
                       if(currentTarget == null ||
                          currentTarget == target)
                        {
                            playerFighter.AttackEnemey(target);
                            return true;
                        }
                        else
                        {
                            currentTarget = target;
                        }
                   }
            }
            return false; // 마우스 위치에 적이 없을 경우          
        }
        
        /// <summary>
        /// 플레이어 컨트롤러 이동 처리 메소드
        /// </summary>
        /// <param name = "ray">레이</param>
        ///<returns> 이동 처리 : TRUE, 레이캐스트 실패 : FALSE </returns>
        private bool InteractWithMovement(Ray ray)
        {
            if (Input.GetMouseButton(0))
            {
                return MoveToCursor(ray);
            }
            return false;
        }

        /// <summary>
        /// 커서의 위치로 이동하게 하는 처리
        /// </summary>
        /// <param name = "ray">레이</param>
        ///<returns> 이동 처리 : TRUE, 레이캐스트 실패 : FALSE </returns>
        private bool MoveToCursor(Ray ray)
        {
            //레이캐스트에 맞은 정보
            RaycastHit hit;
            // 맞았는가 판정하면서 hit에 정보 저장.
            bool hasHit = Physics.Raycast(ray, out hit);

            //레이캐스트에 맞은 정보가 있을 경우
            if(hasHit)
            {
                playerMover.MoveTo(hit.point);
                return true;
            }

            return false; //레이캐스트에 맞은 정보가 없을 경우
        }

    }
}
