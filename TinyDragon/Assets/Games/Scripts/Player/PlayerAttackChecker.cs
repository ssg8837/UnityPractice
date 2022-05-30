using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyDragon;


namespace TinyDragon.Player
{
    public class PlayerAttackChecker : MonoBehaviour
    {
        float damage = 5f;
        float velocitypower = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy.EnemyController enemyController = other.gameObject.GetComponent<Enemy.EnemyController>();

                Vector3 velocity = transform.position;
                enemyController.Attacked(damage, velocitypower, velocity);
            }
        }
    }
}
