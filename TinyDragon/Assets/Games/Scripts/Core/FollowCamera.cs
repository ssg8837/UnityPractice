using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TinyDragon.Core
{
    public class FollowCamera : MonoBehaviour
    {
        private Transform followTarget;

        [SerializeField]
        private float cameraSpeed = 4.5f;
        
        private void Start()
        {
            followTarget = GameObject.FindWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, cameraSpeed);
        }

        public void setCameraSpeed(float aCameraSpeed)
        {
            cameraSpeed = aCameraSpeed;
        }
    }
}

