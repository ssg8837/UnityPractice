using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyDragon.Core;

namespace TinyDragon.Core
{

    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private float damage = 5f;

        //[SerializeField]
        //private Vector3 velocity = Vector3.back;

        [SerializeField]
        private float delayTime = .5f;

        [SerializeField]
        private float pushPower = 100f;

        public float Damage { get => damage; set => damage = value; }
        //public Vector3 Velocity { get => velocity; set => velocity = value; }
        public float DelayTime { get => delayTime; set => delayTime = value; }
        public float PushPower { get => pushPower; set => pushPower = value; }
    }
}