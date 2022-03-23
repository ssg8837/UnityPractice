using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    // 에이전트의 목적지
    [SerializeField]private Transform target;

    // 길을 찾아서 이동할 에이전트
    private NavMeshAgent navAgent;


    private void Awake() 
    {
        // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }

    }

    private void MoveToCursor()
    {
        //마우스 포지션으로 레이캐스트
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //레이캐스트에 맞은 정보
        RaycastHit hit;
        // 맞았는가 판정하면서 hit에 정보 저장.
        bool hasHit = Physics.Raycast(ray, out hit);

        //레이캐스트에 맞은 정보가 있을 경우
        if(hasHit)
        {
            // 에이전트에게 목적지를 알려주는 함수        
            navAgent.SetDestination(hit.point);
        }
    }
}
