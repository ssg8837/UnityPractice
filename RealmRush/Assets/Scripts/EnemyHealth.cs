using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints;
    int currentHitPoints;
    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
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
        }
    }
}
