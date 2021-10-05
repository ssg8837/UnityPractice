using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVfx;
    [SerializeField] Transform parent;
    private void OnParticleCollision(GameObject other) 
    {   
        GameObject vfx= Instantiate(deathVfx, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        //gameObject(스크립트를 컴포넌트로 가지고 있는 게임 오브젝트)
        Destroy(gameObject);
    }

}
