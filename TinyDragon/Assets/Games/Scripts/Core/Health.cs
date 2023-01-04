using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TinyDragon.Core;

namespace TinyDragon.Core
{
    public class Health : MonoBehaviour
    {
        [Tooltip("최대 체력")]
        [SerializeField]
        private float maxHealth = 50f;

        private float currentHealth;
        public Health()
        {
            currentHealth = maxHealth;
        }

        public float MaxHealth { get => maxHealth; }
        public float CurrentHealth { get => currentHealth; }

        public bool healthDamaged(float damage)
        {
            bool isDie = false;

            currentHealth -= damage;

            if(currentHealth <= 0)
            {
                isDie = true;
            }

            return isDie;
        }
    }
}