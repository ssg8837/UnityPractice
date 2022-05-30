using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TinyDragon.Core;
using TinyDragon.Enemy;

namespace TinyDragon.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [Tooltip("최대 체력")]
        [SerializeField]
        private float maxHealth;

        private float currentHealth;
    }
}