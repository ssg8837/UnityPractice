using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    private enum WeaponType
    {
        SubMachinGun,
        Shotgun,
        Carbine
    }
    [SerializeField] WeaponType currentWeapon = WeaponType.SubMachinGun;
    // Start is called before the first frame update
    void Start()
    {
        SetWeaponAcitve();
    }
    // Update is called once per frame
    void Update()
    {
        WeaponType previousWeapon = currentWeapon;

        ProcessKeyInput();
        ProcessScrollWheel();

        if(previousWeapon != currentWeapon)
        {
            SetWeaponAcitve();
        }
    }

    private void ProcessScrollWheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if((int)currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = WeaponType.SubMachinGun;
            }
            else
            {
                currentWeapon++;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if((int)currentWeapon <= 0 )
            {
                currentWeapon =WeaponType.Carbine;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void ProcessKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = WeaponType.SubMachinGun;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = WeaponType.Shotgun;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = WeaponType.Carbine;
        }
    }

    private void SetWeaponAcitve()
    {
        int WeaponIndex = 0;
        foreach(Transform weapon in transform)
        {
            if(WeaponIndex == (int)currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
                WeaponZoom weaponZoom = weapon.GetComponent<WeaponZoom>();
                if(weaponZoom.ZoomFlg)
                {
                    weaponZoom.ZoomOut();
                }
                
            }
            WeaponIndex++;
        }
    }
}
