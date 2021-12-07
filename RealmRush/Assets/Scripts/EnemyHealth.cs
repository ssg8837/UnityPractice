using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EnemyHealth스크립트를 생성할때 필요스크립트 Enemy도 생성되도록함
[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;

    [SerializeField] int maxHitPoints;
    
    [Tooltip("적이 죽을때 체력 추가 되는량")] 
    [SerializeField] int difficultyRamp = 1;
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
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
