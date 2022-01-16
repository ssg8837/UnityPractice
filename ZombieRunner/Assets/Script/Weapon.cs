using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] GameObject hitEffcet;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }
        else
        {
            return;
        }
    }

    private void PlayMuzzleFlash()
    {
        if(muzzleFlash !=null)
        {
            muzzleFlash.Play();
        }
    }

    private void CreateHitImpact(RaycastHit aHit)
    {
        GameObject instantiateHitEffect = Instantiate(hitEffcet, aHit.point, Quaternion.LookRotation(aHit.normal));

        Destroy(instantiateHitEffect, 0.1f);
    }
}
