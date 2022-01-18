using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f; 
    // Start is called before the first frame update

    public void AttackedByEnemy(float damage)
    {
        playerHealth -= damage;
        if(playerHealth <= 0 )
        {
            PlayerDead();
        }
    }

    void PlayerDead()
    {
        Debug.Log("Game Over");
    }
}
