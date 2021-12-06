using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;

    [SerializeField] int maxHitPoints;
    int currentHitPoints;
    // Start is called before the first frame update
    private void OnEnable() 
    {
        currentHitPoints = maxHitPoints;
    }

    private void Start() 
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other) 
    {
        ProcessHit();    
    }

    private void ProcessHit()
    {
        if(currentHitPoints > 0)
        {
            Debug.Log(currentHitPoints);
            currentHitPoints--;
        }
        else
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
    }
}
