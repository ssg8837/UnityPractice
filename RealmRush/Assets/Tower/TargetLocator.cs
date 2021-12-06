using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    Transform target;
    float tragetDistacne;
    //사거리
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem weaponParticle;
     
    // Start is called before the first frame update

    GameObject targetEnemy;

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        GameObject[] enemise = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestTarget = null;
        tragetDistacne = Mathf.Infinity;

        foreach(GameObject enemy in enemise)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if( enemyDistance < tragetDistacne)
            {
                closestTarget = enemy.transform;
                tragetDistacne = enemyDistance;
                targetEnemy = enemy;
            }
        }
    }

    private void AimWeapon()
    {
        weapon.LookAt(targetEnemy.transform);
        if(tragetDistacne < range)
        {
            Attack(targetEnemy);
        }
        else
        {
            Attack(false);
        }
    }

    private void Attack(bool isAlive)
    {   
        var emissionModule= weaponParticle.emission;
        emissionModule.enabled = isAlive;
    }
}
