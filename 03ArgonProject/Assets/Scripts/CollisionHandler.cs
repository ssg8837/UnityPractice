using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log(other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log($"{this.name} Trigeer Enter {other.gameObject.name}");
    }
}
