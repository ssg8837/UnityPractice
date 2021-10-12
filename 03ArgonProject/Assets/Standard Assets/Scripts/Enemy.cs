using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVfx;
    [SerializeField] GameObject hitVfx;
    // [SerializeField] Transform parent;

    [SerializeField] private int HealthPoint = 3;

    private Rigidbody EnemyRigidbody;
    private Transform parent;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parent = GameObject.FindWithTag("SpawnAtRuntime").transform;
        AddRigidBody();
    }

    private void AddRigidBody()
    {
        EnemyRigidbody = gameObject.AddComponent<Rigidbody>();
        EnemyRigidbody.useGravity = false;
        EnemyRigidbody.isKinematic = true;
    }

    ScoreBoard scoreBoard;
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit(100);
    }

    private void ProcessHit(int increaseAmount)
    {
        GameObject vfx = Instantiate(hitVfx, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        scoreBoard.IncreaseScore(increaseAmount);

        HealthPoint--;
        if(HealthPoint <= 0)
        {
            KillEnemy();
        }
    }
    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVfx, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        //gameObject(스크립트를 컴포넌트로 가지고 있는 게임 오브젝트)
        Destroy(gameObject);
    }

}
