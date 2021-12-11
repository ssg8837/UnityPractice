using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] [Range(0,50)]int poolSize = 5;
    [SerializeField] [Range(0,1f)]float spawnTimer = 1f;

    //적 집합
    GameObject[] pool;
 
    private void Awake() 
    {
        PopulatedPool();
    }

    void Start()
    {
        //메소드의 리런값 만큼(여기서는 초) 정기적으로 실행함.
        StartCoroutine(SpawnEnemy());
    }

    void PopulatedPool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemyPrefabs, transform);
            pool[i].SetActive(false);
        }
    }

    //코루틴(Coroutine)에서 호출하기 위한 메소드 : IEnumerator
    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            EnableObjectInPool();
            //IEnumerator메소드에서 리턴하기 위해 yield, 초를 알리기 위해 WaitForSeconds를 사용.
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void EnableObjectInPool()
    {
        //적 오브젝트 생성
        // Instantiate(enemyPrefabs, transform);

        //비활성화 된 애들 찾아서 순서대로 활성화 시킴
        for(int i = 0; i < poolSize; i++)
        {
            //활성화 되어있는가? 비활성화시
            if(pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                //그만 찾고 활성화 시킴
                return;
            }
        }
    }

}
