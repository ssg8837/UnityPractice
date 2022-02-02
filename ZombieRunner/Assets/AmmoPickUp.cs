using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType = AmmoType.Bullets;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUpAmmoInPocket(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void PickUpAmmoInPocket(GameObject player)
    {
        Ammo ammo = player.GetComponent<Ammo>();
        ammo.IncreaseCurrentAmmo(ammoType, ammoAmount);
    }
}
