using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] int poolSize = 5;
    [SerializeField] float spawnTimer = 1f;

    //적 집합
    GameObject[] pool;
 
    int spawnEnemeyCounter = 0;

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
            ActiveEnemy();
            //IEnumerator메소드에서 리턴하기 위해 yield, 초를 알리기 위해 WaitForSeconds를 사용.
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void ActiveEnemy()
    {
            //적 오브젝트 생성
            // Instantiate(enemyPrefabs, transform);
            pool[spawnEnemeyCounter].SetActive(true);
            spawnEnemeyCounter ++;
            if(spawnEnemeyCounter == poolSize)
            {
                spawnEnemeyCounter = 0;
            }
    }

}
