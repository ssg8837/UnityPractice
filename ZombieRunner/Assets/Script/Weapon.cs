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
    [SerializeField] Ammo ammoSlot;
    [SerializeField] float timeBetweenShots = 0.5f;

    bool canShoot = true;
    void Update()
    {
        if(Input.GetMouseButtonDown(0) 
        && canShoot == true
        && ammoSlot.GetCurrentAmmo() > 0)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot =false;
        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceCurrentAmmo();

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot =true;
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
